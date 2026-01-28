using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Flow
{
    public class NodeResponseSourceDTO : BaseDTO
    {
        public Guid NodeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ResponseSourceType SourceType { get; set; }
        public string? StaticResponse { get; set; }
        public Guid? ConnectorId { get; set; }
        public string? ConnectorName { get; set; }
        public bool ProcessWithLLM { get; set; }
        public bool IncludeSourceInfo { get; set; }
        public bool EnableCaching { get; set; }
        public Dictionary<string, object> ClientMetadata { get; set; } = new();
    }
}