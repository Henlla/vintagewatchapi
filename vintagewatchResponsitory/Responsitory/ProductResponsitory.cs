using vintagewatchDAO;
using vintagewatchModel;
using vintagewatchResponsitory.IResponsitory;

namespace vintagewatchResponsitory.Responsitory
{
    public class ProductResponsitory : IProductResponsitory
    {
        private ProductDAO _dao;
        public ProductResponsitory(ProductDAO dao)
        {
            this._dao = dao;
        }
        public Task<List<Products>> GetProducts()
        {
            return _dao.GetProducts();
        }
    }
}
