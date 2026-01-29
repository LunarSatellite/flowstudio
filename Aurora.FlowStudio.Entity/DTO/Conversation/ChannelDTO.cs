using System;
using System.Collections.Generic;
using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Conversation
{
    public class ChannelDTO : TenantBaseDTO
    {
        public string Name { get; set; }
        public ChannelType Type { get; set; }
        public bool IsEnabled { get; set; }
        public Dictionary<string, object> Configuration { get; set; }
        public string WebhookUrl { get; set; }
        public string WebhookSecret { get; set; }
        public int MessageCount { get; set; }
        public DateTime? LastMessageAt { get; set; }
    }
}
