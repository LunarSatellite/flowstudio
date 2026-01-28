using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Integration
{
    public class ConnectorDTO : BaseDTO
    {
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ConnectorType Type { get; set; }
        public ConnectorCategory Category { get; set; }
        public string? IconUrl { get; set; }
        public ConnectorStatus Status { get; set; }
        public HealthStatus? LastHealthStatus { get; set; }
        public DateTime? LastHealthCheckAt { get; set; }
        public int EndpointCount { get; set; }
        public ConnectorMetricsDTO Metrics { get; set; } = new();
    }
}