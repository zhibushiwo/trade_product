/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Data;
using NFine.Domain.Entity.ProductManage;
using NFine.Domain.IRepository.ProductManage;
using NFine.Repository.ProductManage;
using System.Collections.Generic;

namespace NFine.Repository.ProductManage
{
    public class ProductRepository : RepositoryBase<ProductEntity>, IProductRepository
    {
        public void SubmitForm(ProductEntity productEntity, List<ProductSubEntity> listProductSub, string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update(productEntity);
                }
                else
                {
                    db.Insert(productEntity);
                }
                db.Delete<ProductSubEntity>(t => t.F_ParentId == productEntity.F_Id);
                db.Insert(listProductSub);
                db.Commit();
            }
        }
    }
}
