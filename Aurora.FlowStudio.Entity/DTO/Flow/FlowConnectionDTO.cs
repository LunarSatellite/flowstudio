using System;
using Aurora.FlowStudio.Entity.DTO.Base;

namespace Aurora.FlowStudio.Entity.DTO.Flow
{
    public class FlowConnectionDTO : TenantBaseDTO
    {
        public Guid FlowId { get; set; }
        public string SourceNodeId { get; set; }
        public string TargetNodeId { get; set; }
        public string Label { get; set; }
        public string Condition { get; set; }
        public int ExecutionCount { get; set; }
    }
}
