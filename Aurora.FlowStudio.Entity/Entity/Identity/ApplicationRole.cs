using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Aurora.FlowStudio.Entity.Identity
{
    /// <summary>
    /// Application Role extending ASP.NET Core Identity
    /// Tenant-specific roles with custom permissions
    /// </summary>
    [Table("Roles")]
    public class ApplicationRole : IdentityRole<Guid>
    {
        #region Tenant & Description

        /// <summary>
        /// Tenant ID for multi-tenancy (null = system role)
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Role description
        /// </summary>
        [MaxLength(500)]
        public string? Description { get; set; }

        /// <summary>
        /// Is this a system role (cannot be modified/deleted)
        /// </summary>
        public bool IsSystemRole { get; set; }

        /// <summary>
        /// Is role active
        /// </summary>
        public bool IsActive { get; set; }

        #endregion

        #region Permissions

        /// <summary>
        /// List of permission names assigned to this role
        /// </summary>
        [Column(TypeName = "jsonb")]
        public List<string> Permissions { get; set; }

        #endregion

        #region Statistics

        /// <summary>
        /// Number of users with this role
        /// </summary>
        public int UserCount { get; set; }

        #endregion

        #region Audit Fields

        /// <summary>
        /// When role was created
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Who created the role
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// When role was last updated
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Who last updated the role
        /// </summary>
        public Guid? UpdatedBy { get; set; }

        #endregion

        #region Constructor

        public ApplicationRole()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            IsSystemRole = false;
            IsActive = true;
            UserCount = 0;
            Permissions = new List<string>();
        }

        public ApplicationRole(string roleName) : this()
        {
            Name = roleName;
            NormalizedName = roleName.ToUpper();
        }

        #endregion
    }
}
