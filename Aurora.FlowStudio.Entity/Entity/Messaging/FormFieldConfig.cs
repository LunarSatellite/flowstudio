using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Messaging
{
    [Table("FormFieldConfigs", Schema = "msg")]

    [Index(nameof(CreatedAt))]

    public class FormFieldConfig : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Label { get; set; } = string.Empty;
        public FormFieldType Type { get; set; } = FormFieldType.Text;
        public bool Required { get; set; } = false;
        [MaxLength(200)]
        public string? Placeholder { get; set; }
        [MaxLength(200)]
        public string? DefaultValue { get; set; }
        public List<OptionConfig> Options { get; set; } = new();
        public ValidationRule? Validation { get; set; }
    }
}