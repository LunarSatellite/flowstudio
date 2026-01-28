using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Integration
{
    [Table("ParameterDefinitions", Schema = "integration")]

        public class ParameterDefinition
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        public ParameterLocation Location { get; set; } = ParameterLocation.Query;
        public ParameterType Type { get; set; } = ParameterType.String;
        public bool Required { get; set; } = false;
        public object? DefaultValue { get; set; }
        [MaxLength(4000)]
        public string? Description { get; set; }
        [MaxLength(100)]
        public string? ValidationPattern { get; set; }
        public object? MinValue { get; set; }
        public object? MaxValue { get; set; }
        public List<object>? AllowedValues { get; set; }
    }
}