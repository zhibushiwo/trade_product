/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Data;
using System.Data.SqlClient;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    public class AccountController : ControllerBase
    {
        private AccountApp accountApp = new AccountApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson()
        {
            var data = accountApp.GetList();
            var treeList = new List<TreeSelectModel>();
            foreach (AccountEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.F_Id;
                treeModel.text = item.AccountName;
                //treeModel.parentId = item.SupplierId;
                treeModel.data = item;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeJson()
        {
            var data = accountApp.GetList();
            var treeList = new List<TreeViewModel>();
            foreach (AccountEntity item in data)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = data.Count(t => t.AccountId == item.F_Id) == 0 ? false : true;
                tree.id = item.F_Id;
                tree.text = item.AccountName;
                //tree.value = item.F_EnCode;
                //tree.parentId = item.SupplierId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]

        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = accountApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }



        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = accountApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(AccountEntity accountEntity, string keyValue)
        {
            accountApp.SubmitForm(accountEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            accountApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        //[HttpGet]
        //[HandlerAjaxOnly]
        //public string GetDataJson(DataTable table)
        //{
        //    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        //    List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
        //    Dictionary<string, object> childRow;
        //    foreach (DataRow row in table.Rows)
        //    {
        //        childRow = new Dictionary<string, object>();
        //        foreach (DataColumn col in table.Columns)
        //        {
        //            childRow.Add(col.ColumnName, row[col]);
        //        }
        //        parentRow.Add(childRow);
        //    }
        //    return jsSerializer.Serialize(parentRow);
        //}


        //public DataTable GetListDT()
        //{
        //    string connectionString = "Server=.;Initial Catalog=trade_product;Integrated Security=True";
        //    string sql = "select a.* , b.PlatformEnName from dbo.Sys_Account a , dbo.Sys_Platform b where a.PlatformId = b.F_Id";
        //    SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString);
        //    DataSet ds = new DataSet();
        //    adapter.Fill(ds);
        //    DataTable dt = ds.Tables[0];
        //    return dt;
        //}
    }
}
