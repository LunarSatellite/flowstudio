using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("TableSchemas", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class TableSchema : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? DisplayName { get; set; }
        [MaxLength(4000)]
        public string? Description { get; set; }
        public List<ColumnSchema> Columns { get; set; } = new();
        public List<string> PrimaryKeys { get; set; } = new();
        public List<RelationshipSchema> Relationships { get; set; } = new();
        public long? EstimatedRowCount { get; set; }
    }
}