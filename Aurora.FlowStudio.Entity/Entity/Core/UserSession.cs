using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Core
{
    [Table("UserSessions", Schema = "core")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class UserSession : TenantBaseEntity
    {
        public Guid UserId { get; set; }
        [MaxLength(100)]
        public string SessionToken { get; set; } = string.Empty;
        [MaxLength(100)]
        public string RefreshToken { get; set; } = string.Empty;
        [MaxLength(200)]
        public string IpAddress { get; set; } = string.Empty;
        [MaxLength(200)]
        public string UserAgent { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? DeviceId { get; set; }
        [MaxLength(200)]
        public string? DeviceName { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime ExpiresAt { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? LastActivityAt { get; set; }
        public SessionStatus Status { get; set; } = SessionStatus.Active;
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public User User { get; set; } = null!;
    }
}