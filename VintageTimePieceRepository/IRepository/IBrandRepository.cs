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
        public Brand? GetBrandById(int id);

        public Brand? DeleteBrand(int id);
    }
}
