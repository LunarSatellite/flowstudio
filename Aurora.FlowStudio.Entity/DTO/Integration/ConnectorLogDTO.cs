using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Integration
{
    public class ConnectorLogDTO : BaseDTO
    {
        public Guid ConnectorId { get; set; }
        public string ConnectorName { get; set; } = string.Empty;
        public Guid? EndpointId { get; set; }
        public string? EndpointName { get; set; }
        public LogLevel Level { get; set; }
        public string Message { get; set; } = string.Empty;
        public int? StatusCode { get; set; }
        public int DurationMs { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }
}