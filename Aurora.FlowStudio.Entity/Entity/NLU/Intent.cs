using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.NLU
{
    [Table("Intents", Schema = "nlu")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class Intent : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string DisplayName { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        [MaxLength(200)]
        public string Category { get; set; } = string.Empty;
        public IntentStatus Status { get; set; } = IntentStatus.Active;

        // Training Configuration
        public TrainingConfig TrainingSettings { get; set; } = new();
        public int MinConfidenceThreshold { get; set; } = 70; // 0-100

        // Intent Priority
        public int Priority { get; set; } = 0;
        public bool IsFallback { get; set; } = false;

        // Context Requirements
        public List<string> RequiredContexts { get; set; } = new();
        public List<string> ProvidedContexts { get; set; } = new();

        // Follow-up Intents
        public List<Guid> FollowUpIntentIds { get; set; } = new();

        // Action Mapping
        public Guid? TriggeredFlowId { get; set; }
        public Guid? TriggeredNodeId { get; set; }
        [MaxLength(2000)]
        public string? WebhookUrl { get; set; }
        public Dictionary<string, object> ActionPayload { get; set; } = new();

        // Multi-language Support
        [MaxLength(200)]
        public string DefaultLanguage { get; set; } = "en";
        public List<string> SupportedLanguages { get; set; } = new();

        // Training Data
        public int TrainingPhrasesCount { get; set; } = 0;
        [Column(TypeName = "datetime2")]
        public DateTime? LastTrainedAt { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? LastUpdatedAt { get; set; }

        // Performance Metrics
        public IntentMetrics Metrics { get; set; } = new();

        // Tags and Metadata
        public List<string> Tags { get; set; } = new();
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public ICollection<TrainingPhrase> TrainingPhrases { get; set; } = new List<TrainingPhrase>();
        public ICollection<IntentEntity> Entities { get; set; } = new List<IntentEntity>();
        public ICollection<IntentResponse> Responses { get; set; } = new List<IntentResponse>();
        public ICollection<IntentParameter> Parameters { get; set; } = new List<IntentParameter>();
        public ICollection<IntentVersion> Versions { get; set; } = new List<IntentVersion>();
    }
}