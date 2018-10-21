using Newtonsoft.Json;
using NFine.Application.ProductManage;
using NFine.Code;
using NFine.Domain.Entity.ProductManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.ProductDemo.Controllers
{
    public class ProductListController : ControllerBase
    {
        private ProductApp productApp = new ProductApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = productApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        public ActionResult GetGridJsonall()
        {
            int page = Convert.ToInt32(Request["page"].ToString());
            int rows = Convert.ToInt32(Request["rows"].ToString());
            DataSet ds = GetListDT(page, rows);
            DataTable dt1 = ds.Tables[0];
            var result = new { rows = dt1, total = ds.Tables[1].Rows[0]["total"] };
            return Content(result.ToJson());
        }
        public ActionResult GetGridJsonProductSub()
        {

            return Content(null);
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = new
            {
                dataForm = productApp.GetForm(keyValue),
                dataSub = productApp.GetFormSub(keyValue),
            };
            //var data = productApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ProductEntity productEntity, string keyValue, string jsonDate)
        {
            try {
            List<ProductSubEntity> listProductSub = JsonConvert.DeserializeObject<List<ProductSubEntity>>(jsonDate);
            productApp.SubmitForm(productEntity, keyValue, listProductSub);
                return Success("操作成功。");
            }catch(Exception e)
            {
                return Error(e.Message);
            }
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

        public ActionResult importImg(HttpPostedFileBase file)
        {
            if (file != null)
            {
                var severPath = this.Server.MapPath("/files/Img/"); //获取当前虚拟文件路径
                var savePath = Path.Combine(severPath, Common.GuId() + Path.GetExtension(file.FileName)); //拼接保存文件路径
                file.SaveAs(savePath);
                var dataOK = new { code = 1, msg = "/files/Img/"+Path.GetFileName(savePath) };
                return Content(dataOK.ToJson());
            }
            var dataNG = new { code = 0, msg = "上传失败" };
            return Content(dataNG.ToJson());
        }

        
         public DataSet GetListDT(int page, int rows)
            {
                string connectionString = "Server=.;Initial Catalog=trade_product;Integrated Security=True";
                StringBuilder sb = new StringBuilder();
                sb.Append(" WITH paging  as ");
                sb.Append(" (select ROW_NUMBER() OVER(PARTITION BY a.MapSKU ORDER BY a.F_Id) AS rownum, a.F_Id,a.ProductId,a.MapSKU,c.Category_FullName,a.Producttype,a.ProductEnName,a.ProductCnName,a.ProEnDescription,");
                sb.Append("a.ProCnDescription, a.ProKeyword, a.ImgUrl, a.ConsultUrl, a.F_DeleteMark, a.F_EnabledMark, a.F_Description, a.F_CreatorTime,");
                sb.Append("a.F_CreatorUserId, a.F_LastModifyTime, a.F_LastModifyUserId,");
                sb.Append("b.F_ParentId, b.Attribute, b.PictureGuId, b.SKU, d.SupplierName, b.PurchaseAddress, b.HWeight, b.GWeight, b.SWeight, b.Long, b.Wide, b.High,");
                sb.Append("b.PurchaseCost, b.TransportCost, b.OtherCost ");
                sb.Append("from Pro_Product a ");
                sb.Append("left join Pro_ProductSub b on a.F_Id = b.F_ParentId ");
                sb.Append("left join Pro_Category c on a.CategoryId = c.F_Id left join Pro_Supplier d on b.Supplier = d.F_Id) ");
                sb.Append("  SELECT * FROM paging WHERE rownum BETWEEN  " + ((page - 1) * rows + 1) + " AND " + page * rows);

                sb.Append("  ;WITH paging  as ");
                sb.Append(" (select ROW_NUMBER() OVER(PARTITION BY a.MapSKU ORDER BY a.F_Id) AS rownum, a.F_Id,a.ProductId,a.MapSKU,c.Category_FullName,a.Producttype,a.ProductEnName,a.ProductCnName,a.ProEnDescription,");
                sb.Append("a.ProCnDescription, a.ProKeyword, a.ImgUrl, a.ConsultUrl, a.F_DeleteMark, a.F_EnabledMark, a.F_Description, a.F_CreatorTime,");
                sb.Append("a.F_CreatorUserId, a.F_LastModifyTime, a.F_LastModifyUserId,");
                sb.Append("b.F_ParentId, b.Attribute, b.PictureGuId, b.SKU, d.SupplierName, b.PurchaseAddress, b.HWeight, b.GWeight, b.SWeight, b.Long, b.Wide, b.High,");
                sb.Append("b.PurchaseCost, b.TransportCost, b.OtherCost ");
                sb.Append("from Pro_Product a ");
                sb.Append("left join Pro_ProductSub b on a.F_Id = b.F_ParentId ");
                sb.Append("left join Pro_Category c on a.CategoryId = c.F_Id left join Pro_Supplier d on b.Supplier = d.F_Id) ");
                sb.Append("  SELECT MAX(rownum) AS total FROM paging ");
                SqlDataAdapter adapter = new SqlDataAdapter(sb.ToString(), connectionString);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds;
            }

    }
}
    //查询数据源
   