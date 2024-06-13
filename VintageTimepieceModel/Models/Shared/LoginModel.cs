using System.ComponentModel.DataAnnotations;

namespace VintageTimepieceModel.Models.Shared
{
    public class LoginModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
