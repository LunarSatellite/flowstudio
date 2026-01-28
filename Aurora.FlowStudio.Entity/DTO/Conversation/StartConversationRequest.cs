using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Conversation
{
    public class StartConversationRequest
    {
        public Guid FlowId { get; set; }
        public Guid? MenuId { get; set; }
        public Guid? CustomerId { get; set; }
        public ConversationChannel Channel { get; set; } = ConversationChannel.WebChat;
        public string? Language { get; set; }
        public Dictionary<string, object> InitialContext { get; set; } = new();
    }
}