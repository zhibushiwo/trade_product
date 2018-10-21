using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    public class LogisticsController : ControllerBase
    {
            private LogisticsApp logisticsApp = new LogisticsApp();

            [HttpGet]
            [HandlerAjaxOnly]

            public ActionResult GetGridJson(Pagination pagination, string keyword)
            {
                var data = new
                {
                    rows = logisticsApp.GetList(pagination, keyword),
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
                var data = logisticsApp.GetForm(keyValue);
                return Content(data.ToJson());
            }
            [HttpPost]
            [HandlerAjaxOnly]
            [ValidateAntiForgeryToken]
            public ActionResult SubmitForm(LogisticsEntity logisticsEntity, string keyValue)
            {
            logisticsApp.SubmitForm(logisticsEntity, keyValue);
                return Success("操作成功。");
            }
            [HttpPost]
            [HandlerAjaxOnly]
            [HandlerAuthorize]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteForm(string keyValue)
            {
            logisticsApp.DeleteForm(keyValue);
                return Success("删除成功。");
            }

        }
}
