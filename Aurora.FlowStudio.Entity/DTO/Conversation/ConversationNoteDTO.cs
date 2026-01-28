using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Conversation
{
    public class ConversationNoteDTO : BaseDTO
    {
        public Guid ConversationId { get; set; }
        public Guid AgentId { get; set; }
        public string AgentName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public NoteVisibility Visibility { get; set; }
    }
}