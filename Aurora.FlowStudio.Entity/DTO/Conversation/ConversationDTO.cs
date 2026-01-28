using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Conversation
{
    public class ConversationDTO : BaseDTO
    {
        public Guid FlowId { get; set; }
        public string? FlowName { get; set; }
        public Guid? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? SessionId { get; set; }
        public ConversationChannel Channel { get; set; }
        public ConversationStatus Status { get; set; }
        public ConversationPriority Priority { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public int MessageCount { get; set; }
        public Guid? CurrentNodeId { get; set; }
        public Dictionary<string, object> Context { get; set; } = new();
        public List<string> Tags { get; set; } = new();
    }
}