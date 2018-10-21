/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System;
using System.ComponentModel.DataAnnotations;

namespace NFine.Domain.Entity.SystemManage
{
    public class AccountEntity : IEntity<AccountEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }
        public string AccountId { get; set; }
        [StringLength(50)]
        public string AccountName { get; set; }
        [StringLength(5)]
        public string AccountSite { get; set; }
        
        public string PlatformId { get; set; }
        public string PlatformTax { get; set; }
        public bool? F_DeleteMark { get; set; }
        [StringLength(200)]
        public string F_Description { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        public string F_DeleteUserId { get; set; }
    }

}
