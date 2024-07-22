using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;

namespace VintageTimePieceRepository.IRepository
{
    public interface IBrandRepository : IBaseRepository<Brand>
    {
        public Task<Brand?> GetBrandById(int id);

        public Task<Brand?> DeleteBrand(int id);
    }
}
