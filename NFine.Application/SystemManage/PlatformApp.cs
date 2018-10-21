/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFine.Application.SystemManage
{
    public class PlatformApp
    {
        private IPlatformRepository service = new PlatformRepository();



        public List<PlatformEntity> GetList()
        {
            return service.IQueryable().OrderBy(t => t.F_CreatorTime).ToList();
        }
        public List<PlatformEntity> GetList(Pagination pagination, string keyword = "")
        {
            var expression = ExtLinq.True<PlatformEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.PlatformCnName.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }
        public PlatformEntity GetForm(string keyValue)
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
        public void SubmitForm(PlatformEntity platformEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                platformEntity.Modify(keyValue);
                service.Update(platformEntity);
            }
            else
            {
                platformEntity.Create();
                service.Insert(platformEntity);
            }
        }
    }
}
