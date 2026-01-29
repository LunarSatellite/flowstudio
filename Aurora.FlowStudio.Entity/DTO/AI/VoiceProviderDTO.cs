using System;
using System.Collections.Generic;
using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.AI
{
    public class VoiceProviderDTO : TenantBaseDTO
    {
        public string Name { get; set; }
        public VoiceProviderType Provider { get; set; }
        public VoiceType Type { get; set; }
        public string ApiKey { get; set; }
        public Dictionary<string, object> Settings { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public int RequestCount { get; set; }
        public int MinutesUsed { get; set; }
        public decimal TotalCost { get; set; }
    }
}
