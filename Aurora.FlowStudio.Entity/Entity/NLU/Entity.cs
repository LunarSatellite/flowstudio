using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.NLU
{
    [Table("Entities", Schema = "nlu")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class Entity : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string DisplayName { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        public EntityType Type { get; set; } = EntityType.Custom;
        public EntityStatus Status { get; set; } = EntityStatus.Active;

        // System Entity Settings
        public bool IsSystemEntity { get; set; } = false;
        [MaxLength(100)]
        public string? SystemEntityId { get; set; } // @sys.date, @sys.number, etc.

        // Custom Entity Settings
        public EntityMatchMode MatchMode { get; set; } = EntityMatchMode.Exact;
        public bool FuzzyMatching { get; set; } = false;
        public int FuzzyMatchThreshold { get; set; } = 80;

        // Regex Pattern
        [MaxLength(200)]
        public string? RegexPattern { get; set; }

        // Multi-language
        public bool MultiLanguage { get; set; } = true;

        // Automated Expansion
        public bool AutoExpansion { get; set; } = false;

        public List<string> Tags { get; set; } = new();
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public ICollection<EntityEntry> Entries { get; set; } = new List<EntityEntry>();
        public ICollection<IntentEntity> IntentEntities { get; set; } = new List<IntentEntity>();
    }
}