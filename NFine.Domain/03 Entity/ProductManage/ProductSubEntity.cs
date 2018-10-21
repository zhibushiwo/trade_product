using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.ProductManage
{
    public class ProductSubEntity : IEntity<ProductSubEntity>, ICreationAudited, IModificationAudited
    {
        public string F_Id { get; set; }
        public string F_ParentId { get; set; }
        public string Attribute { get; set; }
        public string PictureGuId { get; set; }
        public string SKU { get; set; }
        public string Supplier { get; set; }
        public string PurchaseAddress { get; set; }
        public decimal? HWeight { get; set; }
        public decimal? GWeight { get; set; }
        public decimal? SWeight { get; set; }
        public decimal? Long { get; set; }
        public decimal? Wide { get; set; }
        public decimal? High { get; set; }
        public decimal? PurchaseCost { get; set; }
        public decimal? TransportCost { get; set; }
        public decimal? OtherCost { get; set; }
        public string F_Description { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public string F_LastModifyUserId { get; set; }
    }
}
