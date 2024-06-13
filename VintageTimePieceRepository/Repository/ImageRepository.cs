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

    }
}
