using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Messaging
{
    [Table("MessageTemplateUsages", Schema = "msg")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class MessageTemplateUsage : TenantBaseEntity
    {
        public Guid TemplateId { get; set; }
        public Guid? ConversationId { get; set; }
        public Guid? MessageId { get; set; }
        [MaxLength(200)]
        public string Channel { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Language { get; set; } = string.Empty;
        public Dictionary<string, object> VariablesUsed { get; set; } = new();
        public bool Success { get; set; } = true;
        [MaxLength(4000)]
        public string? ErrorMessage { get; set; }
        public int RenderTimeMs { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime UsedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public MessageTemplate Template { get; set; } = null!;
    }
}