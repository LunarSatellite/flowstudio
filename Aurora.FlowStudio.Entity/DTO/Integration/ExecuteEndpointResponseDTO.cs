using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Integration
{
    public class ExecuteEndpointResponseDTO
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public object? Data { get; set; }
        public string? ErrorMessage { get; set; }
        public int LatencyMs { get; set; }
        public bool FromCache { get; set; }
    }
}