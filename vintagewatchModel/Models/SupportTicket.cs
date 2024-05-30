using System;
using System.Collections.Generic;

#nullable disable

namespace vintagewatchModel.Models
{
    public partial class SupportTicket
    {
        public int TicketId { get; set; }
        public int? UserId { get; set; }
        public int? SupportAgentId { get; set; }
        public string Desciption { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ResovleDate { get; set; }
        public bool? IsResovle { get; set; }

        public virtual User SupportAgent { get; set; }
        public virtual User User { get; set; }
    }
}
