using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.ViewModel
{

    public class Sys_v_AccountModel : IEntity<Sys_v_AccountModel>
    {
        //public AccountEntity AccountE { get; set; }
        public string F_Id { get; set; }
        public string AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountSite { get; set; }
        public string PlatformId { get; set; }
        public string PlatformTax { get; set; }
        public bool? F_DeleteMark { get; set; }
        public string F_Description { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        public string F_DeleteUserId { get; set; }
        public string PlatformEnName { get; set; }
    }
}
