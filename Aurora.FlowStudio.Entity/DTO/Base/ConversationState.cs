
namespace Aurora.FlowStudio.Entity.DTO.Base
{
    public class ConversationState
    {
        public string Status { get; set; } = "active";
        public Guid CurrentNodeId { get; set; }
        public Dictionary<string, object> Variables { get; set; } = new();
        public Dictionary<string, object> Context { get; set; } = new();
        public bool IsComplete { get; set; } = false;
    }
}