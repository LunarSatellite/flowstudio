using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("EpochResults", Schema = "train")]

    [Index(nameof(CreatedAt))]

    public class EpochResult : TenantBaseEntity
    {
        public int Epoch { get; set; }
        public double TrainingLoss { get; set; }
        public double ValidationLoss { get; set; }
        public double TrainingAccuracy { get; set; }
        public double ValidationAccuracy { get; set; }
        public TimeSpan Duration { get; set; }
        public Dictionary<string, double> Metrics { get; set; } = new();
    }
}