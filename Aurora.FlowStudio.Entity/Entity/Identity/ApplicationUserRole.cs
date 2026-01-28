using Microsoft.AspNetCore.Identity;
using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Identity
{
    [Table("ApplicationUserRoles", Schema = "identity")]

        public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Column(TypeName = "datetime2")]
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)]
        public string? AssignedBy { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? ExpiresAt { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation Properties
        public virtual ApplicationUser User { get; set; } = null!;
        public virtual ApplicationRole Role { get; set; } = null!;
    }
}