using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VintageTimepieceModel.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public DateTime? DateJoined { get; set; }

    public string? Avatar { get; set; }

    public bool? IsDel { get; set; }

    public int? RoleId { get; set; }
    [JsonIgnore]
    public virtual ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
    [JsonIgnore]
    public virtual ICollection<FeedbacksUser> FeedbacksUserFeedbackTargets { get; set; } = new List<FeedbacksUser>();
    [JsonIgnore]
    public virtual ICollection<FeedbacksUser> FeedbacksUserUsers { get; set; } = new List<FeedbacksUser>();
    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    [JsonIgnore]
    public virtual ICollection<RatingsTimepiece> RatingsTimepieces { get; set; } = new List<RatingsTimepiece>();

    public virtual Role? Role { get; set; }
    [JsonIgnore]
    public virtual ICollection<SupportTicket> SupportTicketSupportAgents { get; set; } = new List<SupportTicket>();
    [JsonIgnore]
    public virtual ICollection<SupportTicket> SupportTicketUsers { get; set; } = new List<SupportTicket>();
    [JsonIgnore]
    public virtual ICollection<Timepiece> Timepieces { get; set; } = new List<Timepiece>();
}
