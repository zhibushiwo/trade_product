/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Domain.Entity.ProductManage;
using NFine.Domain.IRepository.ProductManage;
using NFine.Repository.ProductManage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFine.Application.ProductManage
{
    public class ListingApp
    {
        private IListingRepository service = new ListingRepository();




        public List<ListingEntity> GetList()
        {
            return service.IQueryable().OrderBy(t => t.F_CreatorTime).ToList();
        }
        public ListingEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            if (service.IQueryable().Count(t => t.ListingId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                service.Delete(t => t.F_Id == keyValue);
            }
        }
        public void SubmitForm(ListingEntity listingEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                listingEntity.Modify(keyValue);
                service.Update(listingEntity);
            }
            else
            {
                listingEntity.Create();
                service.Insert(listingEntity);
            }
        }
    }
}
