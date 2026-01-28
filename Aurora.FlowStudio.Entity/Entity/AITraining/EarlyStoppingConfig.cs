using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("EarlyStoppingConfigs", Schema = "train")]

    [Index(nameof(CreatedAt))]

    public class EarlyStoppingConfig : TenantBaseEntity
    {
        public bool Enabled { get; set; } = true;
        [MaxLength(200)]
        public string MonitorMetric { get; set; } = "val_loss";
        public int Patience { get; set; } = 3;
        public double MinDelta { get; set; } = 0.001;
        [MaxLength(200)]
        public string Mode { get; set; } = "min"; // min or max
    }
}