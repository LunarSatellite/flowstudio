using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("ViewSchemas", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class ViewSchema : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        public List<ColumnSchema> Columns { get; set; } = new();
        [MaxLength(200)]
        public string? Definition { get; set; }
    }
}