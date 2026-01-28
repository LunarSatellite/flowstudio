using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("ModelRegistries", Schema = "train")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class ModelRegistry : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string DisplayName { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        public ModelType ModelType { get; set; } = ModelType.IntentClassification;
        public ModelRegistryStatus Status { get; set; } = ModelRegistryStatus.Registered;

        // Model Information
        [MaxLength(200)]
        public string ModelArchitecture { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Framework { get; set; } = string.Empty; // TensorFlow, PyTorch, etc.
        [MaxLength(200)]
        public string Version { get; set; } = "1.0.0";
        [MaxLength(2000)]
        public string? ModelPath { get; set; }
        public long ModelSizeBytes { get; set; }

        // Training Information
        public Guid? TrainingJobId { get; set; }
        public Guid? DatasetId { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? TrainedAt { get; set; }
        [MaxLength(200)]
        public string? TrainedBy { get; set; }

        // Performance Metrics
        public ModelPerformanceMetrics Performance { get; set; } = new();

        // Deployment
        public bool IsDeployed { get; set; } = false;
        [Column(TypeName = "datetime2")]
        public DateTime? DeployedAt { get; set; }
        [MaxLength(200)]
        public string? DeploymentEndpoint { get; set; }
        public DeploymentEnvironment? Environment { get; set; }

        // Usage Tracking
        public int InferenceCount { get; set; } = 0;
        [Column(TypeName = "datetime2")]
        public DateTime? LastInferenceAt { get; set; }
        public double AverageInferenceTimeMs { get; set; }

        // Tags and Metadata
        public List<string> Tags { get; set; } = new();
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public ICollection<ModelVersion> Versions { get; set; } = new List<ModelVersion>();
        public ICollection<ModelDeployment> Deployments { get; set; } = new List<ModelDeployment>();
    }
}