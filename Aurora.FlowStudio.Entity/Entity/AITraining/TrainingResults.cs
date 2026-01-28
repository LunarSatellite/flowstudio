using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("TrainingResultses", Schema = "train")]

    [Index(nameof(CreatedAt))]

    public class TrainingResults : TenantBaseEntity
    {
        public double FinalTrainingLoss { get; set; }
        public double FinalValidationLoss { get; set; }
        public double FinalTrainingAccuracy { get; set; }
        public double FinalValidationAccuracy { get; set; }
        public double BestValidationLoss { get; set; }
        public int BestEpoch { get; set; }
        public Dictionary<string, double> FinalMetrics { get; set; } = new();
        public List<EpochResult> EpochResults { get; set; } = new();
        public TimeSpan TotalTrainingTime { get; set; }
    }
}