using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Conversation
{
    public class SendMessageRequest
    {
        public Guid ConversationId { get; set; }
        public string Content { get; set; } = string.Empty;
        public MessageType Type { get; set; } = MessageType.Text;
        public Dictionary<string, object> Metadata { get; set; } = new();
    }
}