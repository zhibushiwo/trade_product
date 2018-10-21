using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.ProductManage
{

    public class ExchangeEntity : IEntity<ExchangeEntity>, ICreationAudited, IModificationAudited
    {
        public string F_Id { get; set; }
        public string F_BaseCurrency { get; set; }    //基准币种
        public string F_ExchangeCurrency { get; set; }    //兑换币种
        public decimal F_Exchange { get; set; }    //汇率
        public DateTime? F_CreatorTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public string F_DeleteUserId { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public string F_Description { get; set; }

    }
}
