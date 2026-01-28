using Microsoft.AspNetCore.Identity;
using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Identity
{
    [Table("ApplicationUserClaims", Schema = "identity")]

    [Index(nameof(CreatedAt))]

    public class ApplicationUserClaim : IdentityUserClaim<Guid>
    {
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(200)]
        public string? CreatedBy { get; set; }

        // Navigation Properties
        public virtual ApplicationUser User { get; set; } = null!;
    }
}