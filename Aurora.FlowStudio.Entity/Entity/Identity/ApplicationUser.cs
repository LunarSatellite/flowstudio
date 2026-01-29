using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Aurora.FlowStudio.Entity.Identity
{
    /// <summary>
    /// Application User extending ASP.NET Core Identity
    /// Links to Tenant for multi-tenancy
    /// </summary>
    [Table("Users")]
    public class ApplicationUser : IdentityUser<Guid>
    {
        #region Tenant & Profile

        /// <summary>
        /// Tenant ID for multi-tenancy
        /// </summary>
        [Required]
        public Guid TenantId { get; set; }

        /// <summary>
        /// User's first name
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        /// <summary>
        /// Full name (computed)
        /// </summary>
        [MaxLength(200)]
        public string FullName => $"{FirstName} {LastName}";

        /// <summary>
        /// Profile picture URL
        /// </summary>
        [MaxLength(500)]
        public string? Avatar { get; set; }

        /// <summary>
        /// User's timezone
        /// </summary>
        [MaxLength(50)]
        public string? Timezone { get; set; }

        /// <summary>
        /// Preferred language
        /// </summary>
        [MaxLength(10)]
        public string? Language { get; set; }

        #endregion

        #region Status & Tracking

        /// <summary>
        /// Is user active?
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Is user deleted (soft delete)
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// When was user deleted
        /// </summary>
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// Who deleted the user
        /// </summary>
        public Guid? DeletedBy { get; set; }

        /// <summary>
        /// Last login timestamp
        /// </summary>
        public DateTime? LastLoginAt { get; set; }

        /// <summary>
        /// Last login IP address
        /// </summary>
        [MaxLength(50)]
        public string? LastLoginIp { get; set; }

        /// <summary>
        /// Total login count
        /// </summary>
        public int LoginCount { get; set; }

        /// <summary>
        /// Failed login attempts
        /// </summary>
        public int FailedLoginAttempts { get; set; }

        /// <summary>
        /// When account was locked out due to failed attempts
        /// </summary>
        public DateTime? AccountLockedUntil { get; set; }

        #endregion

        #region Audit Fields

        /// <summary>
        /// When user was created
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Who created the user
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// When user was last updated
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Who last updated the user
        /// </summary>
        public Guid? UpdatedBy { get; set; }

        #endregion

        #region Preferences

        /// <summary>
        /// Email notification preferences
        /// </summary>
        public bool EmailNotifications { get; set; }

        /// <summary>
        /// SMS notification preferences
        /// </summary>
        public bool SmsNotifications { get; set; }

        /// <summary>
        /// Push notification preferences
        /// </summary>
        public bool PushNotifications { get; set; }

        #endregion

        #region Constructor

        public ApplicationUser()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
            IsDeleted = false;
            LoginCount = 0;
            FailedLoginAttempts = 0;
            EmailNotifications = true;
            SmsNotifications = false;
            PushNotifications = true;
        }

        #endregion
    }
}
