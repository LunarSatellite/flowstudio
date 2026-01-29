using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Aurora.FlowStudio.Entity.Identity
{
    /// <summary>
    /// User-Role mapping with audit trail
    /// </summary>
    [Table("UserRoles")]
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        /// <summary>
        /// When was this role assigned to user
        /// </summary>
        [Required]
        public DateTime AssignedAt { get; set; }

        /// <summary>
        /// Who assigned this role to user
        /// </summary>
        public Guid? AssignedBy { get; set; }

        /// <summary>
        /// Is this assignment active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Role assignment expiration (null = never expires)
        /// </summary>
        public DateTime? ExpiresAt { get; set; }

        public ApplicationUserRole()
        {
            AssignedAt = DateTime.UtcNow;
            IsActive = true;
        }
    }
}
