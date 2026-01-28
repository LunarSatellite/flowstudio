using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Integration
{
    public class CreateEndpointRequest
    {
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public EndpointType Type { get; set; }
        public HttpMethodType Method { get; set; } = HttpMethodType.GET;
        public string Path { get; set; } = string.Empty;
        public Dictionary<string, object> Configuration { get; set; } = new();
    }
}