using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("ColumnSchemas", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class ColumnSchema : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? DisplayName { get; set; }
        [MaxLength(200)]
        public string DataType { get; set; } = string.Empty;
        public bool IsNullable { get; set; } = true;
        public bool IsPrimaryKey { get; set; } = false;
        public bool IsForeignKey { get; set; } = false;
        public object? DefaultValue { get; set; }
        public int? MaxLength { get; set; }
        public int? Precision { get; set; }
        public int? Scale { get; set; }
        [MaxLength(4000)]
        public string? Description { get; set; }
    }
}