using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Conversation
{
    [Table("ConversationTags", Schema = "conv")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class ConversationTag : TenantBaseEntity
    {
        public Guid ConversationId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Color { get; set; }
        public TagCategory Category { get; set; } = TagCategory.Custom;

        // Navigation properties
        public Conversation Conversation { get; set; } = null!;
    }
}