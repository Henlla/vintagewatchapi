using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageTimepieceModel.Models.Shared
{
    public class RegisterModel
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }

        public DateTime? JoinedDate { get; set; }
    }
}
