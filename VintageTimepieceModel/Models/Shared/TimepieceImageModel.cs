using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageTimepieceModel.Models.Shared
{
    public class TimepieceImageModel
    {
        public Timepiece? timepiece { get; set; }
        public List<TimepieceImage>? listImage { get; set; }
    }
}
