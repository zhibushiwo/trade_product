/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System;

namespace NFine.Domain.Entity.ProductManage
{
    public class ListingEntity : IEntity<ListingEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }
        public string ListingId { get; set; }

        public string ItemID { get; set; }
        public string SKU { get; set; }
        public string CategoryId { get; set; }
        public string PlatformId { get; set; }
        public string SupplierId { get; set; }
        public string ProductEnName { get; set; }
        public string ProductCnName { get; set; }
        public decimal HWeight { get; set; }//净重
        public decimal GWeight { get; set; }//毛重
        public string ProDescription { get; set; }
        public decimal Long { get; set; }
        public decimal Wide { get; set; }
        public decimal High { get; set; }
        public string ProKeyword { get; set; }
        public string img_url { get; set; }
        public decimal Purchase_price { get; set; }//采购价
        public decimal Purchase_shipping { get; set; }//采购运费
        public decimal Other_shipping { get; set; }//包装费+打印费+杂费
        public decimal Send_Price { get; set; }//派送运费，Killmall=寄送中转仓+寄送头程
        public decimal Sell_Price { get; set; }//售价
        public decimal Pay_shipping { get; set; }//买家支付运费
        public decimal Profit { get; set; }//盈亏

        public DateTime? F_CreatorTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public string F_DeleteUserId { get; set; }
        public bool? F_DeleteMark { get; set; }
        public bool? F_EnabledMark { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public string F_Description { get; set; }
        public string Link { get; set; }
    }
}
