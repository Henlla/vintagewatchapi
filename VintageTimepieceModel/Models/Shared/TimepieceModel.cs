using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageTimepieceModel.Models.Shared
{
    public class TimepieceModel
    {
        public Timepiece? timepiece { get; set; }
        public TimepieceImage? mainImage { get; set; }
        public List<TimepieceImage>? images { get; set; }
    }
}
