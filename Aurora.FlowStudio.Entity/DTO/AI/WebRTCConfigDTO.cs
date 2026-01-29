using System;
using System.Collections.Generic;
using Aurora.FlowStudio.Entity.DTO.Base;

namespace Aurora.FlowStudio.Entity.DTO.AI
{
    public class WebRTCConfigDTO : TenantBaseDTO
    {
        public string Name { get; set; }
        public string StunServer { get; set; }
        public string TurnServer { get; set; }
        public string TurnUsername { get; set; }
        public string TurnPassword { get; set; }
        public Dictionary<string, object> AudioSettings { get; set; }
        public Dictionary<string, object> VideoSettings { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
    }
}
