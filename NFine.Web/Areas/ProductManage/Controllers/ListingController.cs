/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Application.ProductManage;
using NFine.Code;
using NFine.Domain.Entity.ProductManage;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

namespace NFine.Web.Areas.ProductManage.Controllers
{
    public class ListingController : ControllerBase
    {
        private ListingApp listingApp = new ListingApp();

        public ListingController()
        {

        }


        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson()
        {
            var data = listingApp.GetList();
            var treeList = new List<TreeSelectModel>();
            foreach (ListingEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.F_Id;
                treeModel.text = item.ProductCnName;
                treeModel.parentId = item.ListingId;
                treeModel.data = item;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeJson()
        {
            var data = listingApp.GetList();
            var treeList = new List<TreeViewModel>();
            foreach (ListingEntity item in data)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = data.Count(t => t.ListingId == item.F_Id) == 0 ? false : true;
                tree.id = item.F_Id;
                tree.text = item.ProductCnName;
                //tree.value = item.F_EnCode;
                tree.parentId = item.ListingId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }

        //[HttpGet]
        //[HandlerAjaxOnly]
        //public ActionResult GetGridJson(string keyword)
        //{
        //    var data = listingApp.GetList();
        //    return Content(data.ToJson());
        //}

        public ActionResult GetGridJson(string keyword)
        {
            return Content(GetDataJson(GetListDT()));
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public string GetDataJson(DataTable table)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return jsSerializer.Serialize(parentRow);
        }

        public DataTable GetListDT()
        {
            string connectionString = "Server=.;Initial Catalog=trade_product;User ID=Fusion_Trade;Password=123";
            string sql = "select a.* , b.PlatformEnName , c.Category_FullName , d.SupplierName from dbo.Pro_Listing a left join dbo.Sys_Platform b on a.PlatformId = b.F_Id left join dbo.Pro_Category c on a.CategoryId = c.F_Id left join dbo.Pro_Supplier d on a.SupplierId = d.F_Id";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            return dt;
        }


        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = listingApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ListingEntity ListingEntity, string keyValue)
        {
            listingApp.SubmitForm(ListingEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            listingApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
