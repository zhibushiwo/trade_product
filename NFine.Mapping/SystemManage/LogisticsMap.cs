using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.SystemManage
{

    public class LogisticsMap : EntityTypeConfiguration<LogisticsEntity>
    {
        public LogisticsMap()
        {
            this.ToTable("Sys_Logistics");
            this.HasKey(t => t.F_Id);
        }
    }
}
