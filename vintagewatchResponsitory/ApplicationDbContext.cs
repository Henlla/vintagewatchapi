using Microsoft.EntityFrameworkCore;
using vintagewatchModel;

namespace vintagewatchDAO
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Products> Products { get; set; }
    }
}
