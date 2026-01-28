using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("AggregationRules", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class AggregationRule : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Field { get; set; } = string.Empty;
        public AggregationType Type { get; set; } = AggregationType.Count;
        [MaxLength(200)]
        public string? Alias { get; set; }
    }
}