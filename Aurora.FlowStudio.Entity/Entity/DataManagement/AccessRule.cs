using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("AccessRules", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class AccessRule : TenantBaseEntity
    {
        [MaxLength(200)]
        public string RoleName { get; set; } = string.Empty;
        public List<string> AllowedTables { get; set; } = new();
        public List<string> AllowedColumns { get; set; } = new();
        [MaxLength(200)]
        public string? RowFilter { get; set; }
        public List<DataOperation> AllowedOperations { get; set; } = new();
    }
}