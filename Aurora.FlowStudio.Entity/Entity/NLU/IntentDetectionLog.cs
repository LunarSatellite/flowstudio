using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.NLU
{
    [Table("IntentDetectionLogs", Schema = "nlu")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class IntentDetectionLog : TenantBaseEntity
    {
        public Guid ConversationId { get; set; }
        public Guid? MessageId { get; set; }
        [MaxLength(200)]
        public string UserInput { get; set; } = string.Empty;
        public Guid? DetectedIntentId { get; set; }
        public double Confidence { get; set; }
        public List<IntentScore> AlternativeIntents { get; set; } = new();
        public Dictionary<string, EntityValue> ExtractedEntities { get; set; } = new();
        public bool WasCorrect { get; set; } = true;
        public Guid? ActualIntentId { get; set; }
        [MaxLength(200)]
        public string? FeedbackComment { get; set; }
        public int ProcessingTimeMs { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new();
    }
}