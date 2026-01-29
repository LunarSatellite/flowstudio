using System;
using System.Collections.Generic;
using Aurora.FlowStudio.Entity.DTO.Base;

namespace Aurora.FlowStudio.Entity.DTO.Knowledge
{
    public class IntentDTO : TenantBaseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Examples { get; set; }
        public Guid? FlowId { get; set; }
        public int MatchCount { get; set; }
        public decimal Confidence { get; set; }
        public bool IsActive { get; set; }
    }
}
