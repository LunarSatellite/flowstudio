using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Core
{
    [Table("Permissions", Schema = "core")]

    [Index(nameof(CreatedAt))]

    public class Permission : BaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string DisplayName { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        [MaxLength(200)]
        public string Module { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Action { get; set; } = string.Empty;
        public bool IsSystemPermission { get; set; } = false;

        // Navigation properties
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}