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
            bool isSuccess = false;
            if (result != null)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Create category success" : "Create category fail";

            return new APIResponse<Category>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }

        public async Task<APIResponse<Category>> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return new APIResponse<Category>
                {
                    Message = "Don't find the category",
                    isSuccess = false,
                };
            }
            category.IsDel = true;
            var result = await _categoryRepository.Update(category);
            bool isSuccess = false;
            if (result != null)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Delete category success" : "Delete category fail";

            return new APIResponse<Category>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }

        public async Task<APIResponse<List<Category>>> GetAllCategory()
        {
            var result = await _categoryRepository.GetAllCategory();
            bool isSuccess = false;
            if (result.Count > 0)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Get all category success" : "Get all category fail";

            return new APIResponse<List<Category>>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }

        public async Task<APIResponse<Category>> GetCategoryById(int id)
        {
            var result = await _categoryRepository.GetCategoryById(id);
            bool isSuccess = false;
            if (result != null)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Get category success" : "Get category fail";

            return new APIResponse<Category>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }

        public async Task<APIResponse<Category>> UpdateCategory(int id, Category cate)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return new APIResponse<Category>
                {
                    Message = "Update category fail, Category not exists",
                    isSuccess = false,
                    Data = category
                };
            }
            category.CategoryName = cate.CategoryName;
            var result = await _categoryRepository.Update(category);
            bool isSuccess = false;
            if (result != null)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Update category success" : "Update category fail";

            return new APIResponse<Category>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }
    }
}
