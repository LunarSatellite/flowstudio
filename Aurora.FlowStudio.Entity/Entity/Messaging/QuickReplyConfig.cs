using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Messaging
{
    [Table("QuickReplyConfigs", Schema = "msg")]

    [Index(nameof(CreatedAt))]

    public class QuickReplyConfig : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Value { get; set; }
        [MaxLength(2000)]
        public string? IconUrl { get; set; }
        public ContentType ContentType { get; set; } = ContentType.Text;
        public Dictionary<string, object> Payload { get; set; } = new();
    }
}