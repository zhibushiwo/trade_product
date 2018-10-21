/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System;
using System.ComponentModel.DataAnnotations;

namespace NFine.Domain.Entity.ProductManage
{
    public class SupplierEntity : IEntity<SupplierEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }
        public string SupplierId { get; set; }
        [StringLength(50)]
        public string SupplierName { get; set; }
        [StringLength(20)]
        public string ContactName { get; set; }
        public string ContactProvince { get; set; }
        public string ContactCity { get; set; }
        public string ContactDistrict { get; set; }
        public string ContactAddress { get; set; }
        [StringLength(20)]
        public string ContactPhone { get; set; }
        [StringLength(200)]
        public string StoreAddress { get; set; }
        public string WangWang { get; set; }
        public string QQ { get; set; }
        [StringLength(200)]
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
