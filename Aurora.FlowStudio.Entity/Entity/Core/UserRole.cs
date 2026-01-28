using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Core
{
    [Table("UserRoles", Schema = "core")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class UserRole : TenantBaseEntity
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? ExpiresAt { get; set; }

        // Navigation properties
        public User User { get; set; } = null!;
        public Role Role { get; set; } = null!;
    }
}