using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.IRepository;
using VintageTimepieceService.IService;

namespace VintageTimepieceService.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<APIResponse<Category>> CreateNewCategory(Category cate)
        {
            var result = await _categoryRepository.Add(cate);
            return new APIResponse<Category>
            {
                Message = "Create category success",
                isSuccess = true,
                Data = result
            };
        }

        public async Task<APIResponse<Category>> DeleteCategory(int id)
        {
            var result = await Task.FromResult(_categoryRepository.FindAll().Where(c => c.CategoryId == id && c.IsDel == false).FirstOrDefault());
            if (result == null)
            {
                return new APIResponse<Category>
                {
                    Message = "Don't find the category",
                    isSuccess = false,
                };
            }
            result.IsDel = true;
            await _categoryRepository.Update(result);
            return new APIResponse<Category>
            {
                Message = "Delete category success",
                isSuccess = true,
            };
        }

        public async Task<APIResponse<List<Category>>> GetAllCategory()
        {
            var result = await Task.FromResult(_categoryRepository.FindAll().Where(c => c.IsDel == false).ToList());
            return new APIResponse<List<Category>>
            {
                Message = "Get all category success",
                isSuccess = true,
                Data = result
            };
        }

        public async Task<APIResponse<Category>> GetCategoryById(int id)
        {
            var result = await Task.FromResult(_categoryRepository.FindAll().Where(c => c.CategoryId == id && c.IsDel == false).FirstOrDefault());
            if (result != null)
            {
                return new APIResponse<Category>
                {
                    Message = "Get category success",
                    isSuccess = true,
                    Data = result
                };
            }
            return new APIResponse<Category>
            {
                Message = "Get category fail",
                isSuccess = false,
            };
        }

        public async Task<APIResponse<Category>> UpdateCategory(int id, Category cate)
        {
            var result = await Task.FromResult(_categoryRepository.FindAll().Where(c => c.CategoryId == id && c.IsDel == false).FirstOrDefault());
            if (result == null)
            {
                return new APIResponse<Category>
                {
                    Message = "Update category fail, Category not exists",
                    isSuccess = false,
                };
            }
            result.CategoryName = cate.CategoryName;
            await _categoryRepository.Update(result);
            return new APIResponse<Category>
            {
                Message = "Update category success",
                isSuccess = true,
                Data = result
            };
        }
    }
}
