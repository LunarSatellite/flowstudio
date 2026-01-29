using System;
using System.Collections.Generic;
using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Conversation
{
    public class MessageDTO : TenantBaseDTO
    {
        public Guid ConversationId { get; set; }
        public MessageRole Role { get; set; }
        public string Content { get; set; }
        public MessageFormat Format { get; set; }
        public string AudioUrl { get; set; }
        public string ImageUrl { get; set; }
        public int? TokenCount { get; set; }
        public int? CharacterCount { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
        public string DetectedIntent { get; set; }
        public Dictionary<string, string> ExtractedEntities { get; set; }
        public decimal? SentimentScore { get; set; }
    }
}
