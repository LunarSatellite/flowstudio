using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.NLU
{
    [Table("EntityEntries", Schema = "nlu")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class EntityEntry : TenantBaseEntity
    {
        public Guid EntityId { get; set; }
        [MaxLength(200)]
        public string Value { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Language { get; set; } = "en";

        // Synonyms
        public List<string> Synonyms { get; set; } = new();

        // Context
        public Dictionary<string, object> ContextData { get; set; } = new();

        // Usage Tracking
        public int UsageCount { get; set; } = 0;
        [Column(TypeName = "datetime2")]
        public DateTime? LastUsedAt { get; set; }

        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public Entity Entity { get; set; } = null!;
    }
}