using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Messaging
{
    [Table("MessageTemplates", Schema = "msg")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class MessageTemplate : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string DisplayName { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        [MaxLength(200)]
        public string Category { get; set; } = string.Empty; // Greeting, Closing, Question, Error, etc.
        public TemplateType Type { get; set; } = TemplateType.Text;
        public TemplateStatus Status { get; set; } = TemplateStatus.Active;

        // Template Content
        [MaxLength(4000)]
        public string Content { get; set; } = string.Empty;
        public ContentFormat Format { get; set; } = ContentFormat.PlainText;

        // Rich Content
        public RichContentConfig? RichContent { get; set; }

        // Variables
        public List<TemplateVariable> Variables { get; set; } = new();
        public Dictionary<string, object> DefaultVariables { get; set; } = new();

        // Multi-language Support
        [MaxLength(200)]
        public string DefaultLanguage { get; set; } = "en";
        public Dictionary<string, string> Translations { get; set; } = new(); // locale -> translated content

        // Channel-Specific Variations
        public Dictionary<string, ChannelTemplateVariation> ChannelVariations { get; set; } = new();

        // Personalization
        public PersonalizationConfig? Personalization { get; set; }

        // A/B Testing
        public List<TemplateVariant> Variants { get; set; } = new();
        public bool EnableABTesting { get; set; } = false;

        // Conditional Rendering
        public List<TemplateCondition> Conditions { get; set; } = new();

        // Validation
        public ValidationRules? Validation { get; set; }

        // Usage Tracking
        public TemplateMetrics Metrics { get; set; } = new();
        public int UsageCount { get; set; } = 0;
        [Column(TypeName = "datetime2")]
        public DateTime? LastUsedAt { get; set; }

        // Version Control
        public int Version { get; set; } = 1;
        public bool IsPublished { get; set; } = false;
        [Column(TypeName = "datetime2")]
        public DateTime? PublishedAt { get; set; }

        // Tags for organization
        public List<string> Tags { get; set; } = new();
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public ICollection<MessageTemplateVersion> Versions { get; set; } = new List<MessageTemplateVersion>();
        public ICollection<MessageTemplateUsage> UsageHistory { get; set; } = new List<MessageTemplateUsage>();
    }
}