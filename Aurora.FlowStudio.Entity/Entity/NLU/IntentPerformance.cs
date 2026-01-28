using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.NLU
{
    [Table("IntentPerformances", Schema = "nlu")]

    [Index(nameof(CreatedAt))]

    public class IntentPerformance : TenantBaseEntity
    {
        [MaxLength(200)]
        public string IntentName { get; set; } = string.Empty;
        public double Precision { get; set; }
        public double Recall { get; set; }
        public double F1Score { get; set; }
        public int TruePositives { get; set; }
        public int FalsePositives { get; set; }
        public int FalseNegatives { get; set; }
    }
}