using vintagewatchModel;
using vintagewatchResponsitory.IResponsitory;
using vintagewatchService.IService;

namespace vintagewatchService.Service
{
    public class ProductService : IProductService
    {
        private IProductResponsitory _service;
        public ProductService(IProductResponsitory service)
        {
            this._service = service;
        }
        public async Task<List<Products>> GetAllProducts()
        {
            return await _service.GetProducts();
        }
    }
}
