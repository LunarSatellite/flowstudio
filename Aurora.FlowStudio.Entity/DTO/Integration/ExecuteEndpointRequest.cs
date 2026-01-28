using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Integration
{
    public class ExecuteEndpointRequest
    {
        public Guid EndpointId { get; set; }
        public Dictionary<string, object> Parameters { get; set; } = new();
        public Dictionary<string, string> Headers { get; set; } = new();
        public object? Body { get; set; }
    }
}