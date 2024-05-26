using Microsoft.EntityFrameworkCore;
using vintagewatchModel;

namespace vintagewatchDAO
{
    public class ProductDAO
    {
        private readonly ApplicationDbContext _db;
        public ProductDAO(ApplicationDbContext db)
        {
            this._db = db;
        }

        public async Task<List<Products>> GetProducts()
        {
            return await _db.Products.ToListAsync();
        }
    }
}
