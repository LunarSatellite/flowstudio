using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Aurora.FlowStudio.Entity.Identity
{
    /// <summary>
    /// User Claims - stores user-specific claims
    /// </summary>
    [Table("UserClaims")]
    public class ApplicationUserClaim : IdentityUserClaim<Guid>
    {
        /// <summary>
        /// When claim was added
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Who added the claim
        /// </summary>
        public Guid? CreatedBy { get; set; }

        public ApplicationUserClaim()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// User Logins - external login providers (Google, Microsoft, etc.)
    /// </summary>
    [Table("UserLogins")]
    public class ApplicationUserLogin : IdentityUserLogin<Guid>
    {
        /// <summary>
        /// When external login was added
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Last time this external login was used
        /// </summary>
        public DateTime? LastUsedAt { get; set; }

        public ApplicationUserLogin()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// User Tokens - refresh tokens, reset tokens, etc.
    /// </summary>
    [Table("UserTokens")]
    public class ApplicationUserToken : IdentityUserToken<Guid>
    {
        /// <summary>
        /// When token was generated
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// When token expires
        /// </summary>
        public DateTime? ExpiresAt { get; set; }

        /// <summary>
        /// Is token revoked
        /// </summary>
        public bool IsRevoked { get; set; }

        public ApplicationUserToken()
        {
            CreatedAt = DateTime.UtcNow;
            IsRevoked = false;
        }
    }

    /// <summary>
    /// Role Claims - stores role-specific claims
    /// </summary>
    [Table("RoleClaims")]
    public class ApplicationRoleClaim : IdentityRoleClaim<Guid>
    {
        /// <summary>
        /// When claim was added
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Who added the claim
        /// </summary>
        public Guid? CreatedBy { get; set; }

        public ApplicationRoleClaim()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
