using Microsoft.AspNetCore.Identity;
using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Aurora.FlowStudio.Entity.Entity.Core;

namespace Aurora.FlowStudio.Entity.Entity.Identity
{
    [Table("ApplicationRoles", Schema = "identity")]

    [Index(nameof(CreatedAt))]

    public class ApplicationRole : IdentityRole<Guid>
    {
        public Guid TenantId { get; set; }
        [MaxLength(200)]
        public string DisplayName { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        public bool IsSystemRole { get; set; } = false;
        public RoleLevel Level { get; set; } = RoleLevel.User;
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Audit Fields
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Column(TypeName = "datetime2")]
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;

        // Navigation Properties
        public virtual Core.Tenant Tenant { get; set; } = null!;
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();
        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; } = new List<ApplicationRoleClaim>();
        public virtual ICollection<RolePermission> Permissions { get; set; } = new List<RolePermission>();
    }
}