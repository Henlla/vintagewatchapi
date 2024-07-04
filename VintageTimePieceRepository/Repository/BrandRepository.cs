using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimePieceRepository.IRepository;

namespace VintageTimePieceRepository.Repository
{
    public class BrandRepository : BaseRepository<Brand>, IBrandRepository
    {
        public BrandRepository(VintagedbContext context) : base(context)
        {
        }

        public Brand? DeleteBrand(int id)
        {
            var brand = GetBrandById(id);
            if (brand != null)
            {
                brand.IsDel = true;
                return Update(brand);
            }
            return brand;
        }

        public Brand? GetBrandById(int id)
        {
            return FindAll().Where(br => br.BrandId == id && br.IsDel == false).SingleOrDefault();
        }
    }
}
