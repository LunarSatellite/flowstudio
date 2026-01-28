using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AI
{
    [Table("AIProviderLogs", Schema = "ai")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class AIProviderLog : TenantBaseEntity
    {
        public Guid AIProviderId { get; set; }
        public Guid? ConversationId { get; set; }
        public Guid? MessageId { get; set; }
        [MaxLength(200)]
        public string ModelName { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Prompt { get; set; }
        [MaxLength(200)]
        public string? Response { get; set; }
        public int InputTokens { get; set; }
        public int OutputTokens { get; set; }
        public int TotalTokens { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }
        public int LatencyMs { get; set; }
        public bool IsSuccess { get; set; } = true;
        [MaxLength(4000)]
        public string? ErrorMessage { get; set; }
        public Dictionary<string, object> Parameters { get; set; } = new();
        public Dictionary<string, object> Metadata { get; set; } = new();

        public AIProvider Provider { get; set; } = null!;
    }
}