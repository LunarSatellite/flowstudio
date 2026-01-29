using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;

namespace Aurora.FlowStudio.Entity.Identity
{
    /// <summary>
    /// Refresh tokens for JWT authentication
    /// Allows users to get new access tokens without re-login
    /// </summary>
    [Table("RefreshTokens")]
    public class RefreshToken : BaseEntity
    {
        /// <summary>
        /// User who owns this token
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// Tenant ID for multi-tenancy
        /// </summary>
        [Required]
        public Guid TenantId { get; set; }

        /// <summary>
        /// The actual refresh token (hashed)
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Token { get; set; }

        /// <summary>
        /// JWT ID (jti claim from access token)
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string JwtId { get; set; }

        /// <summary>
        /// When token was issued
        /// </summary>
        [Required]
        public DateTime IssuedAt { get; set; }

        /// <summary>
        /// When token expires
        /// </summary>
        [Required]
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// Is token revoked
        /// </summary>
        public bool IsRevoked { get; set; }

        /// <summary>
        /// When was token revoked
        /// </summary>
        public DateTime? RevokedAt { get; set; }

        /// <summary>
        /// Reason for revocation
        /// </summary>
        [MaxLength(500)]
        public string? RevocationReason { get; set; }

        /// <summary>
        /// Is token used (one-time use)
        /// </summary>
        public bool IsUsed { get; set; }

        /// <summary>
        /// When was token used
        /// </summary>
        public DateTime? UsedAt { get; set; }

        /// <summary>
        /// IP address from which token was created
        /// </summary>
        [MaxLength(50)]
        public string? CreatedByIp { get; set; }

        /// <summary>
        /// User agent from which token was created
        /// </summary>
        [MaxLength(500)]
        public string? CreatedByUserAgent { get; set; }

        /// <summary>
        /// Device identifier
        /// </summary>
        [MaxLength(200)]
        public string? DeviceId { get; set; }

        /// <summary>
        /// Token that replaced this one (for token rotation)
        /// </summary>
        public Guid? ReplacedByTokenId { get; set; }

        /// <summary>
        /// Is token valid (not expired, not revoked, not used)
        /// </summary>
        [NotMapped]
        public bool IsValid =>
            !IsRevoked &&
            !IsUsed &&
            DateTime.UtcNow < ExpiresAt;

        public RefreshToken()
        {
            IssuedAt = DateTime.UtcNow;
            ExpiresAt = DateTime.UtcNow.AddDays(7); // 7 days default
            IsRevoked = false;
            IsUsed = false;
        }
    }
}
