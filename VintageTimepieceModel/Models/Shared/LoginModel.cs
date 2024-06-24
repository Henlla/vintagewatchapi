using System.ComponentModel.DataAnnotations;

namespace VintageTimepieceModel.Models.Shared
{
    public class LoginModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
