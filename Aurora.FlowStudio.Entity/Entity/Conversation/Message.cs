using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Conversation
{
    [Table("Messages", Schema = "conv")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class Message : TenantBaseEntity
    {
        public Guid ConversationId { get; set; }
        public Guid? NodeId { get; set; }
        public MessageRole Role { get; set; } = MessageRole.User;
        public MessageType Type { get; set; } = MessageType.Text;
        [MaxLength(4000)]
        public string Content { get; set; } = string.Empty;
        public Dictionary<string, object> RichContent { get; set; } = new();
        public MessageFormat Format { get; set; } = MessageFormat.PlainText;
        [MaxLength(200)]
        public string? Language { get; set; }
        public MessageStatus Status { get; set; } = MessageStatus.Sent;
        [Column(TypeName = "datetime2")]
        public DateTime? DeliveredAt { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? ReadAt { get; set; }
        public Guid? ReplyToMessageId { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new();

        // AI-related fields
        [MaxLength(200)]
        public string? DetectedIntent { get; set; }
        public double? IntentConfidence { get; set; }
        public Dictionary<string, object> ExtractedEntities { get; set; } = new();
        public SentimentAnalysis? Sentiment { get; set; }

        // Navigation properties
        public Conversation Conversation { get; set; } = null!;
        public Flow.FlowNode? Node { get; set; }
        public ICollection<MessageAttachment> Attachments { get; set; } = new List<MessageAttachment>();
        public ICollection<MessageReaction> Reactions { get; set; } = new List<MessageReaction>();
    }
}