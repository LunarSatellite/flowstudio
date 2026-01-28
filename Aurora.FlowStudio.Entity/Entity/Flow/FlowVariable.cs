using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Flow
{
    [Table("FlowVariables", Schema = "flow")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class FlowVariable : TenantBaseEntity
    {
        public Guid FlowId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string DisplayName { get; set; } = string.Empty;
        public VariableType Type { get; set; } = VariableType.String;
        [MaxLength(200)]
        public string? DefaultValue { get; set; }
        public VariableScope Scope { get; set; } = VariableScope.Flow;
        public bool IsRequired { get; set; } = false;
        public bool IsEncrypted { get; set; } = false;
        [MaxLength(100)]
        public string? ValidationRule { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public Flow Flow { get; set; } = null!;
    }
}