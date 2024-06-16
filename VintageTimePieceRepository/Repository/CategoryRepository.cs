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

        public Category CreateNewCategory(Category category)
        {
            return Add(category);
        }

        public Category DeleteCategory(Category category)
        {
            return Update(category);
        }

        public List<Category> GetAllCategory()
        {
            return FindAll().ToList();
        }

        public Category? GetCategoryById(int id)
        {
            return FindAll().Where(c=>c.CategoryId == id).SingleOrDefault();
        }

        public Category UpdateCategory(Category category)
        {
            return Update(category);
        }
    }
}
