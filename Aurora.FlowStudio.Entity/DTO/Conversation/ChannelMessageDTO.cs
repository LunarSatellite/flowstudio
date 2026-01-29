using System;
using System.Collections.Generic;
using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Conversation
{
    public class ChannelMessageDTO : TenantBaseDTO
    {
        public Guid MessageId { get; set; }
        public Guid ChannelId { get; set; }
        public MessageDirection Direction { get; set; }
        public string ExternalMessageId { get; set; }
        public MessageStatus Status { get; set; }
        public Dictionary<string, object> RawData { get; set; }
        public string ErrorMessage { get; set; }
        public int RetryCount { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public DateTime? ReadAt { get; set; }
    }
}
