using System;
using System.Collections.Generic;
using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Flow
{
    public class FlowNodeDTO : TenantBaseDTO
    {
        public Guid FlowId { get; set; }
        public string NodeId { get; set; }
        public NodeType Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<string, object> Configuration { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int ExecutionCount { get; set; }
        public decimal AverageDurationMs { get; set; }
    }
}
