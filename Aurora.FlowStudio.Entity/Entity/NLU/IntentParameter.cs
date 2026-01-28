using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.NLU
{
    [Table("IntentParameters", Schema = "nlu")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class IntentParameter : TenantBaseEntity
    {
        public Guid IntentId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string DisplayName { get; set; } = string.Empty;
        public Guid? EntityId { get; set; }
        public bool IsRequired { get; set; } = false;
        public bool IsList { get; set; } = false;
        [MaxLength(200)]
        public string? DefaultValue { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public Intent Intent { get; set; } = null!;
        public Entity? Entity { get; set; }
    }
}