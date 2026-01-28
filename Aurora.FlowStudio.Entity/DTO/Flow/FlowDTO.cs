using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Flow
{
    public class FlowDTO : BaseDTO
    {
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public FlowType Type { get; set; }
        public FlowCategory Category { get; set; }
        public string? IconUrl { get; set; }
        public FlowStatus Status { get; set; }
        public int Version { get; set; }
        public bool IsTemplate { get; set; }
        public bool IsPublic { get; set; }
        public string? PublishedUrl { get; set; }
        public DateTime? PublishedAt { get; set; }
        public int NodeCount { get; set; }
        public FlowAnalyticsDTO? Analytics { get; set; }
    }
}