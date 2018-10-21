/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Data;
using NFine.Domain.Entity.ProductManage;
using System.Collections.Generic;

namespace NFine.Domain.IRepository.ProductManage
{
    public interface IProductRepository : IRepositoryBase<ProductEntity>
    {
        void SubmitForm(ProductEntity productEntity, List<ProductSubEntity> listProductSub, string keyValue);
    }
}
