using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Conversation
{
    [Table("Conversations", Schema = "conv")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class Conversation : TenantBaseEntity
    {
        public Guid FlowId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? CustomerId { get; set; }
        [MaxLength(100)]
        public string? SessionId { get; set; }
        public ConversationChannel Channel { get; set; } = ConversationChannel.WebChat;
        public ConversationStatus Status { get; set; } = ConversationStatus.Active;
        public ConversationPriority Priority { get; set; } = ConversationPriority.Normal;
        [Column(TypeName = "datetime2")]
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        [Column(TypeName = "datetime2")]
        public DateTime? EndedAt { get; set; }
        public int MessageCount { get; set; } = 0;
        [MaxLength(200)]
        public string? IpAddress { get; set; }
        [MaxLength(200)]
        public string? UserAgent { get; set; }
        [MaxLength(200)]
        public string? DeviceType { get; set; }
        [MaxLength(200)]
        public string? Language { get; set; }
        [MaxLength(200)]
        public string? Country { get; set; }
        public Guid? CurrentNodeId { get; set; }
        public Dictionary<string, object> Context { get; set; } = new();
        public Dictionary<string, object> Variables { get; set; } = new();
        public Dictionary<string, object> Metadata { get; set; } = new();
        public ConversationMetrics Metrics { get; set; } = new();

        // Navigation properties
        public Core.User? User { get; set; }
        public Customer? Customer { get; set; }
        public Flow.Flow Flow { get; set; } = null!;
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public ICollection<ConversationTag> Tags { get; set; } = new List<ConversationTag>();
        public ICollection<ConversationNote> Notes { get; set; } = new List<ConversationNote>();
        public ICollection<ConversationFeedback> Feedbacks { get; set; } = new List<ConversationFeedback>();
    }
}