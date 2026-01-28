using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Conversation
{
    [Table("ConversationFeedbacks", Schema = "conv")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class ConversationFeedback : TenantBaseEntity
    {
        public Guid ConversationId { get; set; }
        public int Rating { get; set; } // 1-5 stars
        [MaxLength(200)]
        public string? Comment { get; set; }
        public Dictionary<string, int> DetailedRatings { get; set; } = new(); // e.g., helpfulness, speed, accuracy
        public FeedbackStatus Status { get; set; } = FeedbackStatus.Received;
        [Column(TypeName = "datetime2")]
        public DateTime? ReviewedAt { get; set; }
        [MaxLength(200)]
        public string? ReviewedBy { get; set; }
        [MaxLength(200)]
        public string? ReviewNotes { get; set; }

        // Navigation properties
        public Conversation Conversation { get; set; } = null!;
    }
}