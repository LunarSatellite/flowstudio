using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.AI
{
    public class ChatMessage
    {
        public string Role { get; set; } = string.Empty; // user, assistant, system
        public string Content { get; set; } = string.Empty;
    }
}