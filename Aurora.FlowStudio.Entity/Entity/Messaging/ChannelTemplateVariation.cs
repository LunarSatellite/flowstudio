using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Messaging
{
    [Table("ChannelTemplateVariations", Schema = "msg")]

    [Index(nameof(CreatedAt))]

    public class ChannelTemplateVariation : TenantBaseEntity
    {
        [MaxLength(200)]
        public string ChannelName { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string Content { get; set; } = string.Empty;
        public RichContentConfig? RichContent { get; set; }
        public Dictionary<string, object> ChannelSpecificSettings { get; set; } = new();
    }
}