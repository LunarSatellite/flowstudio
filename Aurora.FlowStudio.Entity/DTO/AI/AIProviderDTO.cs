using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.AI
{
    public class AIProviderDTO : BaseDTO
    {
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public AIProviderType Type { get; set; }
        public AIProviderStatus Status { get; set; }
        public bool IsPlatformDefault { get; set; }
        public bool IsCustomerConfigurable { get; set; }
        public List<AICapability> Capabilities { get; set; } = new();
        public string DefaultModel { get; set; } = string.Empty;
        public List<string> AvailableModels { get; set; } = new();
        public AIProviderMetricsDTO Metrics { get; set; } = new();
    }
}