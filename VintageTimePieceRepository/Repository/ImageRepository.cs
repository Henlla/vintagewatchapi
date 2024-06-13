using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel;
using VintageTimepieceModel.Models;
using VintageTimePieceRepository.IRepository;

namespace VintageTimePieceRepository.Repository
{
    public class ImageRepository : BaseRepository<TimepieceImage>, IImageRepository
    {
        public ImageRepository(VintagedbContext context) : base(context)
        {
        }

        public async Task<List<TimepieceImage>> GetAllImageWithoutFirstImage(int imageId)
        {
            return await Task.FromResult(FindAll().Where(tm => tm.IsDel == false && tm.TimepieceImageId != imageId).ToList());
        }

        public async Task<List<TimepieceImage>> GetAllProductImage()
        {
            return await Task.FromResult(FindAll().Where(tm => tm.IsDel == false).ToList());
        }

        public Task<TimepieceImage> GetFirstImageOfProduct(int productId)
        {
            return Task.FromResult(FindAll().Where(tm => tm.TimpieceId == productId).First());
        }
    }
}
