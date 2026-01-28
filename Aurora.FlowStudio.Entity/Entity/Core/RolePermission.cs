using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Core
{
    [Table("RolePermissions", Schema = "core")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class RolePermission : TenantBaseEntity
    {
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
        public bool Granted { get; set; } = true;

        // Navigation properties
        public Role Role { get; set; } = null!;
        public Permission Permission { get; set; } = null!;
    }
}