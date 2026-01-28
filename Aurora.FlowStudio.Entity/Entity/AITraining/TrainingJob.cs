using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("TrainingJobs", Schema = "train")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class TrainingJob : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        public Guid DatasetId { get; set; }
        public Guid? ModelId { get; set; }
        public TrainingJobStatus Status { get; set; } = TrainingJobStatus.Pending;

        // Training Configuration
        public TrainingJobConfig Configuration { get; set; } = new();

        // Progress Tracking
        public int ProgressPercentage { get; set; } = 0;
        [MaxLength(200)]
        public string? CurrentStep { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? StartedAt { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? CompletedAt { get; set; }
        public TimeSpan? EstimatedTimeRemaining { get; set; }

        // Results
        public TrainingResults? Results { get; set; }

        // Error Handling
        [MaxLength(4000)]
        public string? ErrorMessage { get; set; }
        [MaxLength(200)]
        public string? ErrorStackTrace { get; set; }
        public int RetryCount { get; set; } = 0;
        public int MaxRetries { get; set; } = 3;

        // Resource Usage
        public ResourceUsage? Resources { get; set; }

        // Artifacts
        [MaxLength(2000)]
        public string? ModelOutputPath { get; set; }
        [MaxLength(2000)]
        public string? LogPath { get; set; }
        [MaxLength(2000)]
        public string? CheckpointPath { get; set; }

        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public TrainingDataset Dataset { get; set; } = null!;
        public ICollection<TrainingJobLog> Logs { get; set; } = new List<TrainingJobLog>();
        public ICollection<TrainingCheckpoint> Checkpoints { get; set; } = new List<TrainingCheckpoint>();
    }
}