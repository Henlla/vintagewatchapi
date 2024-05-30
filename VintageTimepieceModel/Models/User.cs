using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VintageTimepieceModel.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public DateTime? DateJoined { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();

    public virtual ICollection<FeedbacksTimepiece> FeedbacksTimepieces { get; set; } = new List<FeedbacksTimepiece>();

    public virtual ICollection<FeedbacksUser> FeedbacksUserFeedbackTargets { get; set; } = new List<FeedbacksUser>();

    public virtual ICollection<FeedbacksUser> FeedbacksUserUsers { get; set; } = new List<FeedbacksUser>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<RatingsTimepiece> RatingsTimepieces { get; set; } = new List<RatingsTimepiece>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<SupportTicket> SupportTicketSupportAgents { get; set; } = new List<SupportTicket>();

    public virtual ICollection<SupportTicket> SupportTicketUsers { get; set; } = new List<SupportTicket>();

    public virtual ICollection<Timepiece> Timepieces { get; set; } = new List<Timepiece>();
}
