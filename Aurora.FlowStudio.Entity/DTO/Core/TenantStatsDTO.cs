using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Core
{
    public class TenantStatsDTO
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int TotalFlows { get; set; }
        public int PublishedFlows { get; set; }
        public int TotalConversations { get; set; }
        public int ActiveConversations { get; set; }
        public int TotalCustomers { get; set; }
        public Dictionary<string, object> CustomMetrics { get; set; } = new();
    }
}