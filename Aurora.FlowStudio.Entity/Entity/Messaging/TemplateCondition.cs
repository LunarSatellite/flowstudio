using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Messaging
{
    [Table("TemplateConditions", Schema = "msg")]

    [Index(nameof(CreatedAt))]

    public class TemplateCondition : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Expression { get; set; } = string.Empty; // Conditional expression
        public ConditionOperator Operator { get; set; } = ConditionOperator.And;
        [MaxLength(4000)]
        public string? AlternativeContent { get; set; }
        public int Priority { get; set; } = 0;
    }
}