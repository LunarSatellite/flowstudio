using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using Aurora.FlowStudio.Entity.Entity.DataManagement;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("DatasetSamples", Schema = "train")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class DatasetSample : TenantBaseEntity
    {
        public Guid DatasetId { get; set; }
        [MaxLength(4000)]
        public string Text { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Language { get; set; } = "en";

        // Labels
        [MaxLength(200)]
        public string? IntentLabel { get; set; }
        public List<EntityLabel> EntityLabels { get; set; } = new();
        [MaxLength(200)]
        public string? SentimentLabel { get; set; }
        public Dictionary<string, string> CustomLabels { get; set; } = new();

        // Split Assignment
        public DataSplit Split { get; set; } = DataSplit.Training;

        // Quality
        public double QualityScore { get; set; } = 1.0;
        public bool IsValidated { get; set; } = false;
        public bool IsSynthetic { get; set; } = false;

        // Source
        public DataSourceEnum Source { get; set; } = DataSourceEnum.Manual;
        [MaxLength(200)]
        public string? SourceReference { get; set; }

        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public TrainingDataset Dataset { get; set; } = null!;
    }
}