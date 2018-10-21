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
    public class SupplierMap : EntityTypeConfiguration<SupplierEntity>
    {
        public SupplierMap()
        {
            this.ToTable("Pro_Supplier");
            this.HasKey(t => t.F_Id);
        }
    }
}
