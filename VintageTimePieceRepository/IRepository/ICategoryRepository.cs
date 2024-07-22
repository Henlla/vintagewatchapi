using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimePieceRepository.IRepository
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        public Task<Category> CreateNewCategory(Category category);
        public Task<Category> DeleteCategory(Category category);
        public Task<Category?> UpdateCategory(Category category);
        public Task<Category?> GetCategoryById(int id);
        public Task<List<Category>> GetAllCategory();
    }
}
