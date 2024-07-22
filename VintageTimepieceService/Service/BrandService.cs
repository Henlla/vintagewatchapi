using Microsoft.EntityFrameworkCore;
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
            var result = await _brandRepository.Add(brand);
            bool isSuccess = false;
            if (result != null)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Create brand success" : "Create brand fail";

            return new APIResponse<Brand>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }

        public async Task<APIResponse<Brand>> DeleteBrand(int id)
        {
            var result = await _brandRepository.DeleteBrand(id);
            bool isSuccess = false;
            if (result != null)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Delete brand success" : "Delete brand fail";

            return new APIResponse<Brand>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }

        public async Task<APIResponse<List<Brand>>> GetAllBrand()
        {
            var result = await _brandRepository.FindAll().ToListAsync();
            bool isSuccess = false;
            if (result != null)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Get all brand success" : "Don't have any brand";

            return new APIResponse<List<Brand>>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }

        public async Task<APIResponse<Brand>> GetOneBrand(int id)
        {
            var result = await _brandRepository.GetBrandById(id);
            bool isSuccess = false;
            if (result != null)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Get brand success" : "Don't find the brand";

            return new APIResponse<Brand>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }
    }
}
