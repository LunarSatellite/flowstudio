using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Conversation
{
    public class SubmitFeedbackRequest
    {
        public Guid ConversationId { get; set; }
        public int Rating { get; set; } // 1-5
        public string? Comment { get; set; }
        public Dictionary<string, int> DetailedRatings { get; set; } = new();
    }
}