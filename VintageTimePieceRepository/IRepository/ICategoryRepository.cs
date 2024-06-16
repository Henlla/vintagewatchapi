using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimePieceRepository.IRepository
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        public Category CreateNewCategory(Category category);
        public Category DeleteCategory(Category category);
        public Category UpdateCategory(Category category);
        public Category? GetCategoryById(int id);
        public List<Category> GetAllCategory();
    }
}
