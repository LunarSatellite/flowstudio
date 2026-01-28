
namespace Aurora.FlowStudio.Entity.DTO.Base
{
    public class ResponseSourceInfo
    {
        public string SourceType { get; set; } = string.Empty; // API, Database, Static, LLM, etc.
        public bool ProcessedByLLM { get; set; } = false;
        public string? ConnectorName { get; set; }
        public string? EndpointName { get; set; }
        public int? LatencyMs { get; set; }
        public bool FromCache { get; set; } = false;
        public Dictionary<string, object> AdditionalInfo { get; set; } = new();
    }
}