using Microsoft.EntityFrameworkCore;
using vintagewatchDAO;
using vintagewatchModel;
using vintagewatchResponsitory.IResponsitory;

namespace vintagewatchResponsitory.Responsitory
{
    public class ProductResponsitory : IProductResponsitory
    {
        private ApplicationDbContext _db;
        public ProductResponsitory(ApplicationDbContext dao)
        {
            _db = dao;
        }
        public async Task<List<Products>> GetProducts()
        {
            return await _db.Products.ToListAsync();
        }
    }
}
