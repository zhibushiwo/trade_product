using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Application.SystemManage
{
    public class LogisticsApp
    {
        private ILogisticsRepository service = new LogisticsRepository();


        public List<LogisticsEntity> GetList()
        {
            return service.IQueryable().OrderBy(t => t.F_CreatorTime).ToList();
        }
        public List<LogisticsEntity> GetList(Pagination pagination, string keyword = "")
        {
            var expression = ExtLinq.True<LogisticsEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.LogisticsName.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }
        public LogisticsEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            
             service.Delete(t => t.F_Id == keyValue);
            
        }
        public void SubmitForm(LogisticsEntity logisticsEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                logisticsEntity.Modify(keyValue);
                service.Update(logisticsEntity);
            }
            else
            {
                logisticsEntity.Create();
                service.Insert(logisticsEntity);
            }
        }
    }
}
