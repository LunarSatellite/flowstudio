using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Flow
{
    public class FlowAnalyticsDTO
    {
        public int TotalConversations { get; set; }
        public int CompletedConversations { get; set; }
        public int AbandonedConversations { get; set; }
        public double CompletionRate { get; set; }
        public double AverageCompletionTimeSeconds { get; set; }
        public double AverageSatisfactionScore { get; set; }
        public Dictionary<string, int> NodeVisits { get; set; } = new();
    }
}