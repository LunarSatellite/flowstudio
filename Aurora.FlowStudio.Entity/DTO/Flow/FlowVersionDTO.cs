using System;
using System.Collections.Generic;
using Aurora.FlowStudio.Entity.DTO.Base;

namespace Aurora.FlowStudio.Entity.DTO.Flow
{
    public class FlowVersionDTO : TenantBaseDTO
    {
        public Guid FlowId { get; set; }
        public int Version { get; set; }
        public Dictionary<string, object> FlowData { get; set; }
        public string ChangeNotes { get; set; }
        public Guid CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
