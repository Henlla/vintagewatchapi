using System.ComponentModel.DataAnnotations;

namespace VintageTimepieceModel.Models.Shared
{
    public class LoginModel
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
