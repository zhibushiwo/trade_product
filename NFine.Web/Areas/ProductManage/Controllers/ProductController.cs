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
using System.IO;
using System;
using System.Web;

namespace NFine.Web.Areas.ProductManage.Controllers
{
    public class ProductController : ControllerBase
    {
        private ProductApp productApp = new ProductApp();

        public ProductController()
        {

        }


        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson()
        {
            var data = productApp.GetList();
            var treeList = new List<TreeSelectModel>();
            foreach (ProductEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.F_Id;
                treeModel.text = item.ProductCnName;
                treeModel.parentId = item.ProductId;
                treeModel.data = item;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeJson()
        {
            var data = productApp.GetList();
            var treeList = new List<TreeViewModel>();
            foreach (ProductEntity item in data)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = data.Count(t => t.ProductId == item.F_Id) == 0 ? false : true;
                tree.id = item.F_Id;
                tree.text = item.ProductCnName;
                //tree.value = item.F_EnCode;
                tree.parentId = item.ProductId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string keyword)
        {
            var data = productApp.GetList();
            return Content(data.ToJson());
        }
   

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = productApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ProductEntity productEntity, string keyValue)
        {
            productApp.SubmitForm(productEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            productApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
