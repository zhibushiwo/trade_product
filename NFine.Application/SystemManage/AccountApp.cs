/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IMRepository;
using NFine.Domain.IRepository.SystemManage;
using NFine.Domain.ViewModel;
using NFine.Repository.SystemManage;
using NFine.Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFine.Application.SystemManage
{
    public class AccountApp
    {
        private IAccountRepository service = new AccountRepository();
        private IMSys_v_AccountRepository serviceView = new Sys_v_AccountRepository();

        public List<AccountEntity> GetList()
        {
            return service.IQueryable().OrderBy(t => t.F_CreatorTime).ToList();
        }

        public List<Sys_v_AccountModel> GetList(Pagination pagination, string keyword = "")
        {
            var expression = ExtLinq.True<Sys_v_AccountModel>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.AccountName.Contains(keyword));
                expression = expression.Or(t => t.AccountSite.Contains(keyword));
                expression = expression.Or(t => t.PlatformEnName.Contains(keyword));
            }
            return serviceView.FindList(expression, pagination);
        }
        public AccountEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            if (service.IQueryable().Count(t => t.PlatformId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                service.Delete(t => t.F_Id == keyValue);
            }
        }
        public void SubmitForm(AccountEntity accountEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                accountEntity.Modify(keyValue);
                service.Update(accountEntity);
            }
            else
            {
                accountEntity.Create();
                service.Insert(accountEntity);
            }
        }
    }
}
