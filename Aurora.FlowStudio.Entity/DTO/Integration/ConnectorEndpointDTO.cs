using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;
using Aurora.FlowStudio.Entity.Entity.Integration;

namespace Aurora.FlowStudio.Entity.DTO.Integration
{
    public class ConnectorEndpointDTO : BaseDTO
    {
        public Guid ConnectorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public EndpointType Type { get; set; }
        public HttpMethodType Method { get; set; }
        public string Path { get; set; } = string.Empty;
        public Dictionary<string, ParameterDefinition> Parameters { get; set; } = new();
    }
}