using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Flow
{
    public class FlowNodeDTO : BaseDTO
    {
        public Guid FlowId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Label { get; set; }
        public NodeType Type { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public Dictionary<string, object> Configuration { get; set; } = new();
        public Dictionary<string, object> Data { get; set; } = new();
        public bool IsEntryPoint { get; set; }
        public List<NodeResponseSourceDTO> ResponseSources { get; set; } = new();
    }
}