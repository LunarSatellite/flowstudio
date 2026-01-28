using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("TransformationConfigs", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class TransformationConfig : TenantBaseEntity
    {
        public Dictionary<string, string> FieldMappings { get; set; } = new();
        [MaxLength(200)]
        public string? FilterExpression { get; set; }
        public List<AggregationRule> Aggregations { get; set; } = new();
        [MaxLength(200)]
        public string? Script { get; set; }
        [MaxLength(200)]
        public string? ScriptLanguage { get; set; } // JavaScript, Python, etc.
        public Dictionary<string, object> CustomSettings { get; set; } = new();
    }
}