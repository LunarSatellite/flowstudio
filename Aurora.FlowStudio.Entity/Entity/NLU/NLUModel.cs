using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.NLU
{
    [Table("NLUModels", Schema = "nlu")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class NLUModel : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string DisplayName { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        public ModelStatus Status { get; set; } = ModelStatus.Draft;
        [MaxLength(200)]
        public string Language { get; set; } = "en";

        // Model Configuration
        [MaxLength(200)]
        public string ModelType { get; set; } = string.Empty; // BERT, GPT, etc.
        [MaxLength(2000)]
        public string? ModelPath { get; set; }
        [MaxLength(200)]
        public string? ModelVersion { get; set; }
        public TrainingConfig TrainingConfig { get; set; } = new();

        // Training Status
        public TrainingStatus TrainingStatus { get; set; } = TrainingStatus.NotStarted;
        [Column(TypeName = "datetime2")]
        public DateTime? TrainingStartedAt { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? TrainingCompletedAt { get; set; }
        public int TrainingProgress { get; set; } = 0; // 0-100
        [MaxLength(200)]
        public string? TrainingError { get; set; }

        // Model Performance
        public ModelPerformanceMetrics Performance { get; set; } = new();

        // Deployment
        public bool IsDeployed { get; set; } = false;
        [Column(TypeName = "datetime2")]
        public DateTime? DeployedAt { get; set; }
        [MaxLength(200)]
        public string? DeploymentEnvironment { get; set; }

        // Intents included in this model
        public List<Guid> IntentIds { get; set; } = new();
        public int IntentCount { get; set; } = 0;

        // Entities included
        public List<Guid> EntityIds { get; set; } = new();
        public int EntityCount { get; set; } = 0;

        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public ICollection<ModelTrainingLog> TrainingLogs { get; set; } = new List<ModelTrainingLog>();
        public ICollection<ModelEvaluation> Evaluations { get; set; } = new List<ModelEvaluation>();
    }
}