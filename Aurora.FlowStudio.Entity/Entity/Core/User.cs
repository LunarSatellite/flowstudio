using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ConversationEntity = Aurora.FlowStudio.Entity.Entity.Conversation.Conversation;

namespace Aurora.FlowStudio.Entity.Entity.Core
{
    [Table("Users", Schema = "core")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class User : TenantBaseEntity
    {
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? PhoneNumber { get; set; }
        [MaxLength(200)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(200)]
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}".Trim();
        [MaxLength(200)]
        public string? DisplayName { get; set; }
        [MaxLength(2000)]
        public string? AvatarUrl { get; set; }
        [MaxLength(200)]
        public string PasswordHash { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? PasswordSalt { get; set; }
        public bool EmailVerified { get; set; } = false;
        public bool PhoneVerified { get; set; } = false;
        public bool TwoFactorEnabled { get; set; } = false;
        [MaxLength(200)]
        public string? TwoFactorSecret { get; set; }
        public UserStatus Status { get; set; } = UserStatus.Active;
        [Column(TypeName = "datetime2")]
        public DateTime? LastLoginAt { get; set; }
        [MaxLength(200)]
        public string? LastLoginIp { get; set; }
        public int FailedLoginAttempts { get; set; } = 0;
        [Column(TypeName = "datetime2")]
        public DateTime? LockedUntil { get; set; }
        [MaxLength(200)]
        public string? TimeZone { get; set; }
        [MaxLength(200)]
        public string? Language { get; set; }
        public Dictionary<string, object> Preferences { get; set; } = new();
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<UserSession> Sessions { get; set; } = new List<UserSession>();
        public ICollection<ConversationEntity> Conversations { get; set; } = new List<ConversationEntity>();
    }
}