using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vintagewatchModel
{   
    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public string? category { get; set; }
        public decimal? price { get; set; }
    }
}
