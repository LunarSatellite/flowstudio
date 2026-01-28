using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("JoinClauses", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class JoinClause : TenantBaseEntity
    {
        public JoinType Type { get; set; } = JoinType.Inner;
        [MaxLength(200)]
        public string Table { get; set; } = string.Empty;
        [MaxLength(200)]
        public string OnCondition { get; set; } = string.Empty;
    }
}