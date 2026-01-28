using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Integration
{
    public class UpdateConnectorRequest
    {
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public ConnectorStatus? Status { get; set; }
        public Dictionary<string, object>? Configuration { get; set; }
        public Dictionary<string, object>? Authentication { get; set; }
    }
}