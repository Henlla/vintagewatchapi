using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;

namespace VintageTimePieceRepository.IRepository
{
    public interface IImageRepository : IBaseRepository<TimepieceImage>
    {
        public Task<TimepieceImage> GetFirstImageOfProduct(int productId);
        public Task<List<TimepieceImage>> GetAllProductImage();
        public Task<List<TimepieceImage>> GetAllImageWithoutFirstImage(int imageId);
    }
}
