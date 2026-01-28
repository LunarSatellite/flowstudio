using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.NLU
{
    [Table("IntentEntities", Schema = "nlu")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class IntentEntity : TenantBaseEntity
    {
        public Guid IntentId { get; set; }
        public Guid EntityId { get; set; }
        public bool IsRequired { get; set; } = false;
        public bool IsList { get; set; } = false;
        [MaxLength(4000)]
        public string? PromptMessage { get; set; } // Prompt if missing
        public List<string> PromptMessages { get; set; } = new(); // Multiple prompt variations
        public int MaxRetries { get; set; } = 3;
        [MaxLength(200)]
        public string? DefaultValue { get; set; }

        // Validation
        [MaxLength(100)]
        public string? ValidationExpression { get; set; }
        [MaxLength(4000)]
        public string? ValidationErrorMessage { get; set; }

        // Navigation properties
        public Intent Intent { get; set; } = null!;
        public Entity Entity { get; set; } = null!;
    }
}