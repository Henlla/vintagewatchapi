using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.IRepository;
using VintageTimepieceService.IService;

namespace VintageTimepieceService.Service
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }
        public async Task<APIResponse<Brand>> CreateNewBrand(Brand brand)
        {
            var result = await Task.FromResult(_brandRepository.Add(brand));
            if (result != null)
            {
                return new APIResponse<Brand>
                {
                    Message = "Create brand success",
                    isSuccess = true,
                    Data = result
                };
            }
            return new APIResponse<Brand>
            {
                Message = "Create brand fail",
                isSuccess = false,
                Data = result
            };
        }

        public async Task<APIResponse<Brand>> DeleteBrand(int id)
        {
            var result = await Task.FromResult(_brandRepository.DeleteBrand(id));
            if (result != null)
            {
                return new APIResponse<Brand>
                {
                    Message = "Delete brand success",
                    isSuccess = true,
                    Data = result
                };
            }
            return new APIResponse<Brand>
            {
                Message = "Delete brand fail",
                isSuccess = false,
                Data = result
            };
        }

        public async Task<APIResponse<List<Brand>>> GetAllBrand()
        {
            var result = await Task.FromResult(_brandRepository.FindAll().ToList());
            if (result.Count > 0)

                return new APIResponse<List<Brand>>
                {
                    Message = "Get all brand success",
                    isSuccess = true,
                    Data = result
                };
            return new APIResponse<List<Brand>>
            {
                Message = "Don't have any brand",
                isSuccess = false,
                Data = result
            };
        }

        public async Task<APIResponse<Brand>> GetOneBrand(int id)
        {
            var result = await Task.FromResult(_brandRepository.GetBrandById(id));
            if (result == null)
                return new APIResponse<Brand>
                {
                    Message = "Don't find the brand",
                    isSuccess = false,
                    Data = result
                };
            return new APIResponse<Brand>
            {
                Message = "Get brand success",
                isSuccess = true,
                Data = result
            };
        }
    }
}
