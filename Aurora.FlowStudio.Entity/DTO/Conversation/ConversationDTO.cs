using System;
using System.Collections.Generic;
using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Conversation
{
    public class ConversationDTO : TenantBaseDTO
    {
        public Guid? CustomerId { get; set; }
        public ChannelType Channel { get; set; }
        public string SessionId { get; set; }
        public ConversationStatus Status { get; set; }
        public Guid? ActiveFlowId { get; set; }
        public Dictionary<string, object> CurrentState { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public int MessageCount { get; set; }
        public int TokensUsed { get; set; }
        public int VoiceMinutesUsed { get; set; }
        public decimal TotalCost { get; set; }
        public string Language { get; set; }
        public bool IsResolved { get; set; }
        public int? Rating { get; set; }
        public string Feedback { get; set; }
    }
}
