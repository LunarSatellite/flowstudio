using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Messaging
{
    [Table("MessageTemplateVersions", Schema = "msg")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class MessageTemplateVersion : TenantBaseEntity
    {
        public Guid TemplateId { get; set; }
        public int Version { get; set; }
        [MaxLength(4000)]
        public string Content { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? ChangeLog { get; set; }
        [MaxLength(200)]
        public string? ChangedBy { get; set; }
        public bool IsActive { get; set; } = false;
        [MaxLength(200)]
        public string TemplateSnapshot { get; set; } = string.Empty; // JSON snapshot

        // Navigation properties
        public MessageTemplate Template { get; set; } = null!;
    }
}