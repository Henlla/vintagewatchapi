using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vintagewatchModel;

namespace vintagewatchService.IService
{
    public interface IProductService
    {
        public Task<List<Products>> GetAllProducts();
    }
}
