using Microsoft.EntityFrameworkCore;
using VintageTimepieceModel;
using VintageTimepieceModel.Models;
using VintageTimePieceRepository.IRepository;

namespace VintageTimePieceRepository.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(VintagedbContext context) : base(context)
        {
        }

        public async Task<Category> CreateNewCategory(Category category)
        {
            return await Add(category);
        }

        public async Task<Category> DeleteCategory(Category category)
        {
            var categoryData = _context.Categories.Where(cate => cate.CategoryId == category.CategoryId).SingleOrDefault();
            if (categoryData.TimepieceCategories.Any())
            {
                if (categoryData.TimepieceCategories.All(tc => !tc.Timepiece.IsDel.Value))
                {
                    return null;
                }
            }
            return await Update(category);
        }

        public async Task<List<Category>> GetAllCategory()
        {
            return await FindAll().Where(c => c.IsDel == false).ToListAsync();
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            return await FindAll().Where(c => c.CategoryId == id && c.IsDel == false).SingleOrDefaultAsync();
        }

        public async Task<Category?> UpdateCategory(Category category)
        {
            return await Update(category);
        }
    }
}
