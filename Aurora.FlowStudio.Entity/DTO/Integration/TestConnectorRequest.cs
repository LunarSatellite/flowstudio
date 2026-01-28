using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Integration
{
    public class TestConnectorRequest
    {
        public Guid ConnectorId { get; set; }
        public Guid? EndpointId { get; set; }
        public Dictionary<string, object> TestParameters { get; set; } = new();
    }
}