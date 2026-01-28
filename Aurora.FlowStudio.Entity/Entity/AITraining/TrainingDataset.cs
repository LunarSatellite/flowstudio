using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using Aurora.FlowStudio.Entity.Entity.DataManagement;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("TrainingDatasets", Schema = "train")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class TrainingDataset : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string DisplayName { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        public DatasetType Type { get; set; } = DatasetType.IntentClassification;
        public DatasetStatus Status { get; set; } = DatasetStatus.Draft;

        // Dataset Configuration
        [MaxLength(200)]
        public string Language { get; set; } = "en";
        public List<string> SupportedLanguages { get; set; } = new();

        // Data Source
        public DataSourceEnum Source { get; set; } = DataSourceEnum.Manual;
        [MaxLength(2000)]
        public string? SourcePath { get; set; }
        [MaxLength(200)]
        public string? SourceReference { get; set; }

        // Data Statistics
        public int TotalSamples { get; set; } = 0;
        public int TrainingSamples { get; set; } = 0;
        public int ValidationSamples { get; set; } = 0;
        public int TestSamples { get; set; } = 0;
        public double TrainingSplit { get; set; } = 0.7;
        public double ValidationSplit { get; set; } = 0.15;
        public double TestSplit { get; set; } = 0.15;

        // Data Quality
        public DataQualityMetrics Quality { get; set; } = new();

        // Preprocessing
        public PreprocessingConfig Preprocessing { get; set; } = new();

        // Augmentation
        public bool EnableAugmentation { get; set; } = false;
        public AugmentationConfig? Augmentation { get; set; }

        // Versioning
        public int Version { get; set; } = 1;
        [Column(TypeName = "datetime2")]
        public DateTime? LastUpdatedAt { get; set; }

        // Tags
        public List<string> Tags { get; set; } = new();
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public ICollection<DatasetSample> Samples { get; set; } = new List<DatasetSample>();
        public ICollection<DatasetVersion> Versions { get; set; } = new List<DatasetVersion>();
        public ICollection<DatasetValidation> Validations { get; set; } = new List<DatasetValidation>();
    }
}