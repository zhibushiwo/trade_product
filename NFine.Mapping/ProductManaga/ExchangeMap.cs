using NFine.Domain.Entity.ProductManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.ProductManaga
{

    public class ExchangeMap : EntityTypeConfiguration<ExchangeEntity>
    {
        public ExchangeMap()
        {
            this.ToTable("Pro_Exchange");
            this.HasKey(t => t.F_Id);
        }
    }
}
