using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageTimepieceModel.Models.Shared
{
    public class VNPayRequestModel
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}
