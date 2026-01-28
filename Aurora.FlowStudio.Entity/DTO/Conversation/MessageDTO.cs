using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Conversation
{
    public class MessageDTO : BaseDTO
    {
        public Guid ConversationId { get; set; }
        public Guid? NodeId { get; set; }
        public MessageRole Role { get; set; }
        public MessageType Type { get; set; }
        public string Content { get; set; } = string.Empty;
        public Dictionary<string, object> RichContent { get; set; } = new();
        public MessageStatus Status { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public DateTime? ReadAt { get; set; }
        public List<MessageAttachmentDTO> Attachments { get; set; } = new();
        public SentimentDTO? Sentiment { get; set; }
    }
}