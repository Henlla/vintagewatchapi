namespace VintageTimepieceModel.Models.Shared
{
    public class TimepieceViewModel
    {
        public Timepiece? timepiece { get; set; }
        public TimepieceImage? mainImage { get; set; }
        public List<TimepieceCategory>? category { get; set; }
        public List<TimepieceImage>? images { get; set; }
        public Evaluation? evaluation { get; set; }
    }
}
