using System;
using System.Collections.Generic;
using Aurora.FlowStudio.Entity.DTO.Base;

namespace Aurora.FlowStudio.Entity.DTO.Conversation
{
    public class CustomerDTO : TenantBaseDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ExternalId { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Timezone { get; set; }
        public Dictionary<string, object> Profile { get; set; }
        public Dictionary<string, object> Preferences { get; set; }
        public DateTime FirstContactAt { get; set; }
        public DateTime LastContactAt { get; set; }
        public int TotalConversations { get; set; }
        public decimal TotalSpent { get; set; }
        public decimal LifetimeValue { get; set; }
        public List<string> Tags { get; set; }
    }
}
