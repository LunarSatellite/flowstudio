using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Conversation
{
    [Table("ConversationNotes", Schema = "conv")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class ConversationNote : TenantBaseEntity
    {
        public Guid ConversationId { get; set; }
        public Guid AgentId { get; set; }
        [MaxLength(4000)]
        public string Content { get; set; } = string.Empty;
        public NoteVisibility Visibility { get; set; } = NoteVisibility.Internal;

        // Navigation properties
        public Conversation Conversation { get; set; } = null!;
        public Core.User Agent { get; set; } = null!;
    }
}