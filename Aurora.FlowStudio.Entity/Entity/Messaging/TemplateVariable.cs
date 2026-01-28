using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Messaging
{
    [Table("TemplateVariables", Schema = "msg")]

    [Index(nameof(CreatedAt))]

    public class TemplateVariable : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string DisplayName { get; set; } = string.Empty;
        public VariableType Type { get; set; } = VariableType.String;
        [MaxLength(200)]
        public string? DefaultValue { get; set; }
        public bool Required { get; set; } = false;
        [MaxLength(4000)]
        public string? Description { get; set; }
        [MaxLength(200)]
        public string? FormatString { get; set; } // e.g., "{0:C}" for currency
        public List<string>? AllowedValues { get; set; }
        public ValidationRule? Validation { get; set; }
    }
}