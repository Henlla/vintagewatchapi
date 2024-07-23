using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageTimepieceModel.Models.Shared
{
    public class VNPayDataModel
    {
        public string vnp_Version { get; set; } = "2.1.0";
        public string vnp_Command { get; set; } = "pay";
        public string vnp_TmnCode { get; set; }
        public string vnp_BankCode { get; set; }
        public string vnp_Locale { get; set; } = "vn";
        public string vnp_CurrCode { get; set; } = "VND";
        public string vnp_TxnRef { get; set; }
        public string vnp_OrderInfo { get; set; }
        public string vnp_OrderType { get; set; } = "other";
        public string vnp_Amount { get; set; }
        public string vnp_ReturnUrl { get; set; }
        public string vnp_CreateDate { get; set; }
        public string vnp_ExpireDate { get; set; }

        // Bill
        public string vnp_Bill_Mobile { get; set; }
        public string vnp_Bill_Email { get; set; }
        public string vnp_Bill_FirstName { get; set; }
        public string vnp_Bill_LastName { get; set; }
        public string vnp_Bill_Address { get; set; }

        // Invoice
        public string vnp_Inv_Phone { get; set; }
        public string vnp_Inv_Email { get; set; }
        public string vnp_Inv_Customer { get; set; }
        public string vnp_Inv_Address { get; set; }
        public string vnp_Inv_Company { get; set; }
        public string vnp_Inv_Type { get; set; } = "I";
    }
}
