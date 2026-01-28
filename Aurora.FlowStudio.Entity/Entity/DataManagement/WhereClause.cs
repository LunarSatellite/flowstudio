using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("WhereClauses", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class WhereClause : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Field { get; set; } = string.Empty;
        public ComparisonOperator Operator { get; set; } = ComparisonOperator.Equal;
        public object? Value { get; set; }
        public LogicalOperator LogicalOperator { get; set; } = LogicalOperator.And;
    }
}