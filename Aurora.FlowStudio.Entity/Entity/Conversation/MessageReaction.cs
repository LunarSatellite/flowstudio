using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Conversation
{
    [Table("MessageReactions", Schema = "conv")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class MessageReaction : TenantBaseEntity
    {
        public Guid MessageId { get; set; }
        public Guid? UserId { get; set; }
        [MaxLength(200)]
        public string Emoji { get; set; } = string.Empty;
        public ReactionType Type { get; set; } = ReactionType.Like;

        // Navigation properties
        public Message Message { get; set; } = null!;
        public Core.User? User { get; set; }
    }
}