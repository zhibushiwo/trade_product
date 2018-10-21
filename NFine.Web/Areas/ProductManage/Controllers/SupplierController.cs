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

namespace NFine.Web.Areas.ProductManage.Controllers
{
    public class SupplierController : ControllerBase
    {
        private SupplierApp supplierApp = new SupplierApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson()
        {
            var data = supplierApp.GetList();
            var treeList = new List<TreeSelectModel>();
            foreach (SupplierEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.F_Id;
                treeModel.text = item.SupplierName;
                treeModel.parentId = item.SupplierId;
                treeModel.data = item;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeJson()
        {
            var data = supplierApp.GetList();
            var treeList = new List<TreeViewModel>();
            foreach (SupplierEntity item in data)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = data.Count(t => t.SupplierId == item.F_Id) == 0 ? false : true;
                tree.id = item.F_Id;
                tree.text = item.SupplierName;
                //tree.value = item.F_EnCode;
                tree.parentId = item.SupplierId;
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
                rows = supplierApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJsonAll(string keyword)
        {
            var data = supplierApp.GetList();
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = supplierApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(SupplierEntity supplierEntity, string keyValue)
        {
            supplierApp.SubmitForm(supplierEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            supplierApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
