using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AI
{
    [Table("AIProviders", Schema = "ai")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class AIProvider : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string DisplayName { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        public AIProviderType Type { get; set; } = AIProviderType.PlatformDefault;
        public AIProviderStatus Status { get; set; } = AIProviderStatus.Active;
        public bool IsPlatformDefault { get; set; } = false;
        public bool IsCustomerConfigurable { get; set; } = false;

        public AIProviderConfig Configuration { get; set; } = new();
        public List<AICapability> Capabilities { get; set; } = new();
        [MaxLength(200)]
        public string DefaultModel { get; set; } = string.Empty;
        public List<string> AvailableModels { get; set; } = new();
        public PricingConfig? Pricing { get; set; }
        public RateLimitInfo RateLimits { get; set; } = new();
        public AIProviderMetrics Metrics { get; set; } = new();
        public Dictionary<string, object> Metadata { get; set; } = new();

        public ICollection<AIModel> Models { get; set; } = new List<AIModel>();
        public ICollection<AIProviderLog> Logs { get; set; } = new List<AIProviderLog>();
    }
}