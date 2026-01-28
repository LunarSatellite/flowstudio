using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Conversation
{
    public class AddConversationNoteRequest
    {
        public Guid ConversationId { get; set; }
        public string Content { get; set; } = string.Empty;
        public NoteVisibility Visibility { get; set; } = NoteVisibility.Internal;
    }
}