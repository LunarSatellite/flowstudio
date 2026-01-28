using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("DatasetVersions", Schema = "train")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class DatasetVersion : TenantBaseEntity
    {
        public Guid DatasetId { get; set; }
        public int Version { get; set; }
        [MaxLength(200)]
        public string? ChangeLog { get; set; }
        public int SampleCount { get; set; }
        public DataQualityMetrics Quality { get; set; } = new();
        [MaxLength(2000)]
        public string? SnapshotPath { get; set; }

        // Navigation properties
        public TrainingDataset Dataset { get; set; } = null!;
    }
}