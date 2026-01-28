using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.AI
{
    public class AIModelDTO : BaseDTO
    {
        public Guid AIProviderId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string ModelId { get; set; } = string.Empty;
        public ModelType Type { get; set; }
        public bool IsActive { get; set; }
        public int MaxTokens { get; set; }
        public int ContextWindow { get; set; }
        public bool SupportsFunctionCalling { get; set; }
        public bool SupportsVision { get; set; }
        public bool SupportsStreaming { get; set; }
        public decimal? InputCostPer1K { get; set; }
        public decimal? OutputCostPer1K { get; set; }
    }
}