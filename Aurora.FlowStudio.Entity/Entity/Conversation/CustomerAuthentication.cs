using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Conversation
{
    [Table("CustomerAuthentications", Schema = "conv")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class CustomerAuthentication : TenantBaseEntity
    {
        public Guid CustomerId { get; set; }
        public CustomerAuthType Type { get; set; } = CustomerAuthType.Anonymous;
        [MaxLength(100)]
        public string? Identifier { get; set; } // Email, Phone, or unique ID
        [MaxLength(200)]
        public string? PasswordHash { get; set; }
        public bool EmailVerified { get; set; } = false;
        public bool PhoneVerified { get; set; } = false;

        // FIDO2/WebAuthn Support
        [MaxLength(100)]
        public string? CredentialId { get; set; }
        [MaxLength(100)]
        public string? PublicKey { get; set; }
        public int SignCounter { get; set; } = 0;
        [MaxLength(100)]
        public string? AaguidInfo { get; set; }
        public List<string> Transports { get; set; } = new();

        // Biometric
        public bool BiometricEnabled { get; set; } = false;
        [MaxLength(200)]
        public string? BiometricType { get; set; } // Fingerprint, FaceID, etc.

        // Session management
        [MaxLength(100)]
        public string? SessionToken { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? LastLoginAt { get; set; }
        [MaxLength(200)]
        public string? LastLoginIp { get; set; }
        [MaxLength(200)]
        public string? DeviceFingerprint { get; set; }
        public int FailedAttempts { get; set; } = 0;
        [Column(TypeName = "datetime2")]
        public DateTime? LockedUntil { get; set; }

        // OAuth/Social
        [MaxLength(100)]
        public string? OAuthProvider { get; set; }
        [MaxLength(100)]
        public string? OAuthUserId { get; set; }
        [MaxLength(100)]
        public string? OAuthAccessToken { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? OAuthTokenExpiry { get; set; }

        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public Customer Customer { get; set; } = null!;
    }
}