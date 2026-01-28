using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Integration
{
    public class CreateConnectorRequest
    {
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ConnectorType Type { get; set; }
        public ConnectorCategory Category { get; set; }
        public Dictionary<string, object> Configuration { get; set; } = new();
        public Dictionary<string, object> Authentication { get; set; } = new();
    }
}