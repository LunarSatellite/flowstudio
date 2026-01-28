using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.AI
{
    public class AIProviderMetricsDTO
    {
        public long TotalRequests { get; set; }
        public long SuccessfulRequests { get; set; }
        public long FailedRequests { get; set; }
        public long TotalTokensUsed { get; set; }
        public long TotalInputTokens { get; set; }
        public long TotalOutputTokens { get; set; }
        public decimal TotalCost { get; set; }
        public double AverageLatencyMs { get; set; }
        public DateTime? LastRequestAt { get; set; }
    }
}