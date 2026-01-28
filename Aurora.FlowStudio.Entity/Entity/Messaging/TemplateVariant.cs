using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Messaging
{
    [Table("TemplateVariants", Schema = "msg")]

    [Index(nameof(CreatedAt))]

    public class TemplateVariant : BaseEntity
    {
        public Guid TemplateId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string Content { get; set; } = string.Empty;
        public RichContentConfig? RichContent { get; set; }
        public int Weight { get; set; } = 50; // Traffic percentage
        public bool IsControl { get; set; } = false;
        public VariantMetrics Metrics { get; set; } = new();
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public MessageTemplate Template { get; set; } = null!;
    }
}