using Microsoft.AspNetCore.Identity;
using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Aurora.FlowStudio.Entity.Entity.Core;

namespace Aurora.FlowStudio.Entity.Entity.Identity
{
    [Table("ApplicationUsers", Schema = "identity")]

    [Index(nameof(CreatedAt))]

    public class ApplicationUser : IdentityUser<Guid>
    {
        // Extended Properties
        [MaxLength(200)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(200)]
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}".Trim();
        [MaxLength(200)]
        public string? DisplayName { get; set; }
        [MaxLength(2000)]
        public string? AvatarUrl { get; set; }

        // Tenant/Company Association
        public Guid TenantId { get; set; }

        // Profile Information
        [MaxLength(200)]
        public string? JobTitle { get; set; }
        [MaxLength(200)]
        public string? Department { get; set; }
        [MaxLength(200)]
        public string? TimeZone { get; set; }
        [MaxLength(200)]
        public string? Language { get; set; }
        [MaxLength(200)]
        public string? Country { get; set; }

        // Status
        public UserStatus Status { get; set; } = UserStatus.Active;

        // FIDO2 Authentication - NO PASSWORD!
        public bool FIDO2Enabled { get; set; } = true; // Always true for studio users
        public bool HasFIDO2Credential { get; set; } = false;
        [Column(TypeName = "datetime2")]
        public DateTime? FIDO2EnrolledAt { get; set; }

        // Session Tracking
        [Column(TypeName = "datetime2")]
        public DateTime? LastLoginAt { get; set; }
        [MaxLength(200)]
        public string? LastLoginIp { get; set; }
        [MaxLength(200)]
        public string? LastUserAgent { get; set; }
        public int FailedLoginAttempts { get; set; } = 0;

        // Preferences
        public Dictionary<string, object> Preferences { get; set; } = new();
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Audit Fields
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Column(TypeName = "datetime2")]
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(200)]
        public string? CreatedBy { get; set; }
        [MaxLength(200)]
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        [Column(TypeName = "datetime2")]
        public DateTime? DeletedAt { get; set; }
        [MaxLength(200)]
        public string? DeletedBy { get; set; }

        // Navigation Properties
        public virtual Core.Tenant Tenant { get; set; } = null!;
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();
        public virtual ICollection<ApplicationUserClaim> UserClaims { get; set; } = new List<ApplicationUserClaim>();
        public virtual ICollection<ApplicationUserLogin> UserLogins { get; set; } = new List<ApplicationUserLogin>();
        public virtual ICollection<ApplicationUserToken> UserTokens { get; set; } = new List<ApplicationUserToken>();
        public virtual ICollection<UserSession> Sessions { get; set; } = new List<UserSession>();
        public virtual ICollection<UserActivityLog> ActivityLogs { get; set; } = new List<UserActivityLog>();
        public virtual ICollection<FIDO2Credential> FIDO2Credentials { get; set; } = new List<FIDO2Credential>();
    }
}