using NFine.Code;
using NFine.Domain.Entity.ProductManage;
using NFine.Domain.IRepository.ProductMangage;
using NFine.Repository.ProductManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Application.ProductManage
{
    public class ExchangeApp
    {
        private IExchangeRepository service = new ExchangeRepository();
        public List<ExchangeEntity> GetList()
        {
            return service.IQueryable().OrderBy(t => t.F_CreatorTime).ToList();
        }

        public List<ExchangeEntity> GetList(Pagination pagination, string keyword = "")
        {
            var expression = ExtLinq.True<ExchangeEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_BaseCurrency.Contains(keyword));
                expression = expression.Or(t => t.F_ExchangeCurrency.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }


        public ExchangeEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {

              service.Delete(t => t.F_Id == keyValue);

        }
        public void SubmitForm(ExchangeEntity exchangeEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                exchangeEntity.Modify(keyValue);
                service.Update(exchangeEntity);
            }
            else
            {
                exchangeEntity.Create();
                service.Insert(exchangeEntity);
            }
        }
    }

}
