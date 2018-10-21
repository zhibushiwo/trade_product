/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System;

namespace NFine.Domain.Entity.ProductManage
{
    public class ProductEntity : IEntity<ProductEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }
        public string ProductId { get; set; }
        public string MapSKU { get; set; }
        public string CategoryId { get; set; }
        public int? Producttype { get; set; }
        public string ProductEnName { get; set; }
        public string ProductCnName { get; set; }
        public string ProEnDescription { get; set; }
        public string ProCnDescription { get; set; }
        public string ProKeyword { get; set; }
        public string ImgUrl { get; set; }
        public string ConsultUrl { get; set; }
        public string F_Description { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public string F_DeleteUserId { get; set; }
        public bool? F_DeleteMark { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public string F_LastModifyUserId { get; set; }
    }
}
