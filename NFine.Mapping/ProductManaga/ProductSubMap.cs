using NFine.Domain.Entity.ProductManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.ProductManaga
{
    public class ProductSubMap : EntityTypeConfiguration<ProductSubEntity>
    {
        public ProductSubMap()
        {
            this.ToTable("Pro_ProductSub");
            this.HasKey(t => t.F_Id);
        }
    }
}
