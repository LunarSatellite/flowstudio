using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("TrainingCheckpoints", Schema = "train")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class TrainingCheckpoint : TenantBaseEntity
    {
        public Guid TrainingJobId { get; set; }
        public int Epoch { get; set; }
        public double ValidationLoss { get; set; }
        public double ValidationAccuracy { get; set; }
        [MaxLength(2000)]
        public string CheckpointPath { get; set; } = string.Empty;
        public long FileSizeBytes { get; set; }
        public bool IsBestModel { get; set; } = false;
        public Dictionary<string, double> Metrics { get; set; } = new();

        // Navigation properties
        public TrainingJob TrainingJob { get; set; } = null!;
    }
}