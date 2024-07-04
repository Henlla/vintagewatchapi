using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimepieceService.IService
{
    public interface IBrandService
    {
        public Task<APIResponse<Brand>> GetOneBrand(int id);
        public Task<APIResponse<List<Brand>>> GetAllBrand();

        public Task<APIResponse<Brand>> CreateNewBrand(Brand brand);
        public Task<APIResponse<Brand>> DeleteBrand(int id);
    }
}
