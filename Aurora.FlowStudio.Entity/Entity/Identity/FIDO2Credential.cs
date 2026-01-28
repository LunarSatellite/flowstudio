using Microsoft.AspNetCore.Identity;
using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Identity
{
    [Table("FIDO2Credentials", Schema = "identity")]

    [Index(nameof(CreatedAt))]

    public class FIDO2Credential : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }

        // FIDO2/WebAuthn Core Fields
        public byte[] CredentialId { get; set; } = Array.Empty<byte>();
        [MaxLength(100)]
        public string CredentialIdBase64 { get; set; } = string.Empty;
        public byte[] PublicKey { get; set; } = Array.Empty<byte>();
        public uint SignCounter { get; set; } = 0;
        public byte[] UserHandle { get; set; } = Array.Empty<byte>();

        // Authenticator Information
        public Guid AaGuid { get; set; } // Authenticator Attestation GUID
        [MaxLength(200)]
        public string? AuthenticatorName { get; set; }
        [MaxLength(200)]
        public string? AuthenticatorModel { get; set; }
        public CredentialType Type { get; set; } = CredentialType.PlatformAuthenticator;
        public List<string> Transports { get; set; } = new(); // usb, nfc, ble, internal

        // Attestation
        [MaxLength(200)]
        public string? AttestationType { get; set; } // none, indirect, direct, enterprise
        public byte[]? AttestationObject { get; set; }
        [MaxLength(200)]
        public string? AttestationFormat { get; set; } // packed, tpm, android-key, etc.

        // Device Information
        [MaxLength(200)]
        public string? DeviceName { get; set; }
        [MaxLength(200)]
        public string? Browser { get; set; }
        [MaxLength(200)]
        public string? OS { get; set; }
        [MaxLength(200)]
        public string? IpAddress { get; set; }

        // Usage Tracking
        [Column(TypeName = "datetime2")]
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
        [Column(TypeName = "datetime2")]
        public DateTime? LastUsedAt { get; set; }
        public int UsageCount { get; set; } = 0;
        public bool IsBackupEligible { get; set; } = false;
        public bool IsBackedUp { get; set; } = false;

        // Status
        public CredentialStatus Status { get; set; } = CredentialStatus.Active;
        public bool IsPrimary { get; set; } = false;
        [Column(TypeName = "datetime2")]
        public DateTime? RevokedAt { get; set; }
        [MaxLength(200)]
        public string? RevokedReason { get; set; }

        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public virtual ApplicationUser User { get; set; } = null!;
        public virtual Core.Tenant Tenant { get; set; } = null!;
    }
}