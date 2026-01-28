using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Integration
{
    public class ConnectorTestResultDTO
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string? Response { get; set; }
        public string? ErrorMessage { get; set; }
        public int LatencyMs { get; set; }
    }
}