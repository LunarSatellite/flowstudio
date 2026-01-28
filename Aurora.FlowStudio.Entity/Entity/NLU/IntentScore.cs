using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.NLU
{
    [Table("IntentScores", Schema = "nlu")]

    [Index(nameof(CreatedAt))]

    public class IntentScore : TenantBaseEntity
    {
        public Guid IntentId { get; set; }
        [MaxLength(200)]
        public string IntentName { get; set; } = string.Empty;
        public double Score { get; set; }
    }
}