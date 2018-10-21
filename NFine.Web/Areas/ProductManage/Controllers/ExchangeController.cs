using NFine.Application.ProductManage;
using NFine.Code;
using NFine.Domain.Entity.ProductManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.ProductManage.Controllers
{
    public class ExchangeController : ControllerBase
    {
        private ExchangeApp exchangeApp = new ExchangeApp();
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = exchangeApp.GetList(pagination, keyword),
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
            var data = exchangeApp.GetForm(keyValue);
            return Content(data.ToJson());
        } 
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ExchangeEntity exchangeEntity, string keyValue)
        {
            exchangeApp.SubmitForm(exchangeEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            exchangeApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
