using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Conversation
{
    [Table("MessageAttachments", Schema = "conv")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class MessageAttachment : TenantBaseEntity
    {
        public Guid MessageId { get; set; }
        [MaxLength(200)]
        public string FileName { get; set; } = string.Empty;
        [MaxLength(2000)]
        public string FileUrl { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string ContentType { get; set; } = string.Empty;
        public long FileSize { get; set; }
        [MaxLength(2000)]
        public string? ThumbnailUrl { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public int? Duration { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public Message Message { get; set; } = null!;
    }
}