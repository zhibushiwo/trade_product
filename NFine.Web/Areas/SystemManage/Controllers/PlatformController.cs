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

namespace NFine.Web.Areas.SystemManage.Controllers
{
    public class PlatformController : ControllerBase
    {
        private PlatformApp platformApp = new PlatformApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson()
        {
            var data = platformApp.GetList();
            var treeList = new List<TreeSelectModel>();
            foreach (PlatformEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.F_Id;
                treeModel.text = item.PlatformCnName;
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
            var data = platformApp.GetList();
            var treeList = new List<TreeViewModel>();
            foreach (PlatformEntity item in data)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = data.Count(t => t.PlatformCode == item.F_Id) == 0 ? false : true;
                tree.id = item.F_Id;
                tree.text = item.PlatformCnName;
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
        public ActionResult GetGridJson(string keyword)
        {
            var data = platformApp.GetList();
            return Content(data.ToJson());
        }

        //[HttpGet]
        //[HandlerAjaxOnly]

        //public ActionResult GetGridJson(Pagination pagination, string keyword)
        //{
        //    var data = new
        //    {
        //        rows = platformApp.GetList(pagination, keyword),
        //        total = pagination.total,
        //        page = pagination.page,
        //        records = pagination.records
        //    };
        //    return Content(data.ToJson());
        //}


        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = platformApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(PlatformEntity platformEntity, string keyValue)
        {
            platformApp.SubmitForm(platformEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            platformApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
