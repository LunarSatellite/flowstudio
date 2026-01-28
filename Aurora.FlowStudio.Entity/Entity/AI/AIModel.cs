using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AI
{
    [Table("AIModels", Schema = "ai")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
    [Index(nameof(CreatedAt))]

    public class AIModel : TenantBaseEntity
    {
        public Guid AIProviderId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string DisplayName { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        [MaxLength(100)]
        public string ModelId { get; set; } = string.Empty;
        public ModelType Type { get; set; } = ModelType.ChatCompletion;
        public bool IsActive { get; set; } = true;
        public int MaxTokens { get; set; } = 4096;
        public int ContextWindow { get; set; } = 4096;
        public bool SupportsFunctionCalling { get; set; } = false;
        public bool SupportsVision { get; set; } = false;
        public bool SupportsStreaming { get; set; } = true;
        public ModelParameters DefaultParameters { get; set; } = new();
        [Column(TypeName = "decimal(18,2)")]
        public decimal? InputCostPer1K { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? OutputCostPer1K { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new();

        public AIProvider Provider { get; set; } = null!;
    }
}