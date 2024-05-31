using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VintageTimepieceModel.Models;

public partial class RefreshToken
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Token { get; set; }

    public string? JwtId { get; set; }

    public bool? IsUsed { get; set; }

    public bool? IsRevoke { get; set; }

    public DateTime? IssueAt { get; set; }

    public DateTime? ExpiredAt { get; set; }
    [JsonIgnore]
    public virtual User?  User { get; set; }
}
