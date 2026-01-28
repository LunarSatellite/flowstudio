using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("APIParameterSchemas", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class APIParameterSchema : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Location { get; set; } = string.Empty; // path, query, header, body
        [MaxLength(200)]
        public string Type { get; set; } = string.Empty;
        public bool Required { get; set; } = false;
        [MaxLength(4000)]
        public string? Description { get; set; }
    }
}