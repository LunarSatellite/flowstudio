using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Messaging
{
    [Table("RichContentConfigs", Schema = "msg")]

    [Index(nameof(CreatedAt))]

    public class RichContentConfig : TenantBaseEntity
    {
        // Card Content
        [MaxLength(200)]
        public string? Title { get; set; }
        [MaxLength(200)]
        public string? Subtitle { get; set; }
        [MaxLength(2000)]
        public string? ImageUrl { get; set; }
        [MaxLength(2000)]
        public string? ThumbnailUrl { get; set; }
        [MaxLength(2000)]
        public string? VideoUrl { get; set; }
        [MaxLength(2000)]
        public string? AudioUrl { get; set; }

        // Buttons
        public List<ButtonConfig> Buttons { get; set; } = new();

        // Quick Replies
        public List<QuickReplyConfig> QuickReplies { get; set; } = new();

        // List Items (for List template)
        public List<ListItemConfig> ListItems { get; set; } = new();

        // Carousel Items (for Carousel template)
        public List<CarouselItemConfig> CarouselItems { get; set; } = new();

        // Form Fields (for Form template)
        public List<FormFieldConfig> FormFields { get; set; } = new();

        // Receipt Data (for Receipt template)
        public ReceiptConfig? Receipt { get; set; }

        // Custom Properties
        public Dictionary<string, object> CustomProperties { get; set; } = new();
    }
}