using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Flow
{
    public class FlowConnectionDTO : BaseDTO
    {
        public Guid FlowId { get; set; }
        public Guid SourceNodeId { get; set; }
        public Guid TargetNodeId { get; set; }
        public string? Label { get; set; }
        public ConnectionType Type { get; set; }
        public Dictionary<string, object> Condition { get; set; } = new();
    }
}