using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.NLU
{
    [Table("TrainingPhrases", Schema = "nlu")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class TrainingPhrase : TenantBaseEntity
    {
        public Guid IntentId { get; set; }
        [MaxLength(4000)]
        public string Text { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Language { get; set; } = "en";
        public PhraseType Type { get; set; } = PhraseType.Example;
        public bool IsTemplate { get; set; } = false;

        // Entity Annotations
        public List<EntityAnnotation> Annotations { get; set; } = new();

        // Quality Metrics
        public double? QualityScore { get; set; }
        public int UsageCount { get; set; } = 0;
        [Column(TypeName = "datetime2")]
        public DateTime? LastUsedAt { get; set; }

        // Source
        public PhraseSource Source { get; set; } = PhraseSource.Manual;
        [MaxLength(200)]
        public string? SourceReference { get; set; }

        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public Intent Intent { get; set; } = null!;
    }
}