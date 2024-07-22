using Microsoft.EntityFrameworkCore;
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

        public async Task<Brand?> DeleteBrand(int id)
        {
            var brand = await GetBrandById(id);
            if (brand != null)
            {
                brand.IsDel = true;
                return await Update(brand);
            }
            return brand;
        }

        public async Task<Brand?> GetBrandById(int id)
        {
            return await FindAll().Where(br => br.BrandId == id && br.IsDel == false).SingleOrDefaultAsync();
        }
    }
}
