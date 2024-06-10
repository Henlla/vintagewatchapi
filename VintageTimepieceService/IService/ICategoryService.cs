using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimepieceService.IService
{
    public interface ICategoryService
    {
        public Task<APIResponse<Category>> CreateNewCategory(Category cate);
        public Task<APIResponse<Category>> DeleteCategory(int id);
        public Task<APIResponse<Category>> UpdateCategory(int id, Category category);
        public Task<APIResponse<Category>> GetCategoryById(int id);
        public Task<APIResponse<List<Category>>> GetAllCategory();

    }
}
