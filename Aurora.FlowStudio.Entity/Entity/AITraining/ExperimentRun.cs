using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("ExperimentRuns", Schema = "train")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class ExperimentRun : TenantBaseEntity
    {
        public Guid ExperimentId { get; set; }
        [MaxLength(200)]
        public string RunName { get; set; } = string.Empty;
        public ExperimentRunStatus Status { get; set; } = ExperimentRunStatus.Running;
        [Column(TypeName = "datetime2")]
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        [Column(TypeName = "datetime2")]
        public DateTime? CompletedAt { get; set; }
        public Dictionary<string, object> Parameters { get; set; } = new();
        public Dictionary<string, object> Metrics { get; set; } = new();
        [MaxLength(2000)]
        public string? ArtifactPath { get; set; }

        // Navigation properties
        public Experiment Experiment { get; set; } = null!;
    }
}