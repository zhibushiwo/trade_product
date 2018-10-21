using NFine.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.ViewModel
{

    public class Sys_v_AccountMap : EntityTypeConfiguration<Sys_v_AccountModel>
    {
        public Sys_v_AccountMap()
        {
            this.ToTable("Sys_v_Account");
            this.HasKey(t => t.F_Id);
        }
    }
}
