/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Domain.Entity.ProductManage;
using NFine.Domain.IRepository.ProductManage;
using NFine.Repository.ProductManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using NFine.Code;

namespace NFine.Application.ProductManage
{
    public class SupplierApp
    {
        private ISupplierRepository service = new SupplierRepository();


        //public string GetDBList()
        //{
        //    StringBuilder sql = new StringBuilder();
        //    sql.Append("select top 1 *  from Pro_Supplier");
        //    SqlCommand cmd = new SqlCommand(sql.ToString());
        //    SqlDataAdapter Adap = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    Adap.Fill(ds);

        //    return cmd.ExecuteNonQuery().ToString();
        //}

        public List<SupplierEntity> GetList()
        {
            return service.IQueryable().OrderBy(t => t.F_CreatorTime).ToList();
        }

        public List<SupplierEntity> GetList(Pagination pagination, string keyword = "")
        {
            var expression = ExtLinq.True<SupplierEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.SupplierName.Contains(keyword));
                expression = expression.Or(t => t.ContactName.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }


        public SupplierEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            if (service.IQueryable().Count(t => t.SupplierId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                service.Delete(t => t.F_Id == keyValue);
            }
        }
        public void SubmitForm(SupplierEntity supplierEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                supplierEntity.Modify(keyValue);
                service.Update(supplierEntity);
            }
            else
            {
                supplierEntity.Create();
                service.Insert(supplierEntity);
            }
        }
    }
}
