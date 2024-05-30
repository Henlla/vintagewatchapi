using System;
using System.Collections.Generic;

#nullable disable

namespace vintagewatchModel.Models
{
    public partial class User
    {
        public User()
        {
            Evaluations = new HashSet<Evaluation>();
            FeedbacksTimepieces = new HashSet<FeedbacksTimepiece>();
            FeedbacksUserFeedbackTargets = new HashSet<FeedbacksUser>();
            FeedbacksUserUsers = new HashSet<FeedbacksUser>();
            Orders = new HashSet<Order>();
            RatingsTimepieces = new HashSet<RatingsTimepiece>();
            SupportTicketSupportAgents = new HashSet<SupportTicket>();
            SupportTicketUsers = new HashSet<SupportTicket>();
            Timepieces = new HashSet<Timepiece>();
            UserRoles = new HashSet<UserRole>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime? DateJoined { get; set; }

        public virtual ICollection<Evaluation> Evaluations { get; set; }
        public virtual ICollection<FeedbacksTimepiece> FeedbacksTimepieces { get; set; }
        public virtual ICollection<FeedbacksUser> FeedbacksUserFeedbackTargets { get; set; }
        public virtual ICollection<FeedbacksUser> FeedbacksUserUsers { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<RatingsTimepiece> RatingsTimepieces { get; set; }
        public virtual ICollection<SupportTicket> SupportTicketSupportAgents { get; set; }
        public virtual ICollection<SupportTicket> SupportTicketUsers { get; set; }
        public virtual ICollection<Timepiece> Timepieces { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
