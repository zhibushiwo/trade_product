using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SqlClientHelper
{
    public class Cmd
    {
        protected bool storedProc;
        protected StringBuilder sqlBuilder;
        protected List<SqlParameter> paramList;

        public Cmd()
        {
            sqlBuilder = new StringBuilder();
            paramList = new List<SqlParameter>();
        }

        public static Cmd New { get { return new Cmd(); } }

        public static Cmd CallProc(string procName, params object[] namedArgs)
        {
            var cmd = new Cmd();
            cmd.storedProc = true;
            cmd.sqlBuilder.Append(procName);
            return cmd.Params(namedArgs);
        }

        public Cmd Script(string sqlfile, params object[] args)
        {
            return Sql(File.ReadAllText(sqlfile), args);
        }

        public Cmd Sql(string sql, params object[] args)
        {
            sqlBuilder.Append(sql);
            if (args.Length > 0)
                paramList.AddSqlParams(sql, args);
            return this;
        }

        public Cmd Params(params object[] namedArgs)
        {
            var idx = 1;
            while (idx < namedArgs.Length)
            {
                var paramName = namedArgs[idx - 1] as string;
                if (paramList.Find(p => p.ParameterName == paramName) == null)
                    paramList.Add(new SqlParameter(paramName, namedArgs[idx] ?? DBNull.Value));
                idx += 2;
            }
            return this;
        }

        protected SqlCommand newSqlCommand(SqlConnection conn, SqlTransaction trans = null)
        {
            if (conn.State != ConnectionState.Open) conn.Open();
            var cmd = new SqlCommand(sqlBuilder.ToString(), conn, trans);
            cmd.CommandType = storedProc ? CommandType.StoredProcedure : CommandType.Text;
            foreach (var param in paramList) cmd.Parameters.Add(param);
            return cmd;
        }

        public int Exec(string connStr)
        {
            using (var conn = new SqlConnection(connStr))
                return Exec(conn);
        }

        public int Exec(SqlConnection conn, SqlTransaction trans = null)
        {
            using (var cmd = newSqlCommand(conn, trans))
                return cmd.ExecuteNonQuery();
        }

        public object Scalar(string connStr)
        {
            using (var conn = new SqlConnection(connStr))
                return Scalar(conn);
        }

        public object Scalar(SqlConnection conn, SqlTransaction trans = null)
        {
            using (var cmd = newSqlCommand(conn, trans))
                return cmd.ExecuteScalar();
        }

        public T Scalar<T>(string connStr)
        {
            using (var conn = new SqlConnection(connStr))
                return Scalar<T>(conn);
        }

        public T Scalar<T>(SqlConnection conn, SqlTransaction trans = null)
        {
            using (var cmd = newSqlCommand(conn, trans))
            {
                var val = cmd.ExecuteScalar();
                return (val != null && val != DBNull.Value) ? (T)val : default(T);
            }
        }

        public void Reader(string connStr, Action<SqlDataReader> readRows)
        {
            using (var conn = new SqlConnection(connStr))
                Reader(conn, readRows);
        }

        public void Reader(SqlConnection conn, Action<SqlDataReader> readRows, SqlTransaction trans = null)
        {
            using (var cmd = newSqlCommand(conn, trans))
            using (var reader = cmd.ExecuteReader())
                readRows(reader);
        }

        public override string ToString()
        {
            return sqlBuilder.ToString();
        }
    }

    public static class SqlParameterListExt
    {
        public static void AddSqlParams(this List<SqlParameter> paramsList, string sql, params object[] args)
        {
            var idx = 0;
            foreach (var paramName in sql.ParamNames())
            {
                if (paramsList.Find(p => p.ParameterName == paramName) == null)
                {
                    if (idx < args.Length)
                        paramsList.Add(new SqlParameter(paramName, args[idx++] ?? DBNull.Value));
                    else
                        throw new ArgumentException("insufficient arguments", nameof(args));
                }
            }
            if (idx < args.Length)
                throw new ArgumentException("too many arguments", nameof(args));
        }
    }

    public static class StringExt
    {
        //https://docs.microsoft.com/en-us/sql/relational-databases/databases/database-identifiers?view=sql-server-2017
        public static IEnumerable<string> ParamNames(this string sql)
        {
            var idx = 0;
            while (idx < sql.Length)
            {
                if (sql[idx] == '@')
                {
                    var start = idx++;
                    if (idx < sql.Length && sql[idx] != '@')
                    {
                        while (idx < sql.Length && (char.IsLetterOrDigit(sql[idx]) ||
                            sql[idx] == '_' || sql[idx] == '$' || sql[idx] == '#' || sql[idx] == '@'))
                            idx++;

                        if (idx <= sql.Length && idx > start + 1)
                            yield return sql.Substring(start, idx - start);
                    }
                }
                else if (sql[idx] == '\'' || sql[idx] == '"')
                {
                    var sep = sql[idx++];
                    while (idx < sql.Length && sql[idx] != sep)
                        idx++;
                }
                else if (sql[idx] == '/' && idx < sql.Length - 1 && sql[idx + 1] == '*')
                {
                    idx += 2;
                    while (idx < sql.Length && sql[idx - 1] != '*' && sql[idx] != '/')
                        idx++;
                }
                idx++;
            }
        }
    }
}
