/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Domain.Entity.ProductManage;
using System.Data.Entity.ModelConfiguration;

namespace NFine.Mapping.ProductManage
{
    public class ProductMap : EntityTypeConfiguration<ProductEntity>
    {
        public ProductMap()
        {
            this.ToTable("Pro_Product");
            this.HasKey(t => t.F_Id);
        }
    }
}
