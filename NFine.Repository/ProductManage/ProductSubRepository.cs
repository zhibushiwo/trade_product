using NFine.Data;
using NFine.Domain.Entity.ProductManage;
using NFine.Domain.IRepository.ProductMangage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Repository.ProductManage
{
    public class ProductSubRepository : RepositoryBase<ProductSubEntity>, IProductSubRepository
    {
    }
}
