using System;
using System.Collections.Generic;
using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Flow
{
    public class FlowExecutionDTO : TenantBaseDTO
    {
        public Guid FlowId { get; set; }
        public Guid ConversationId { get; set; }
        public string CurrentNodeId { get; set; }
        public Dictionary<string, object> Variables { get; set; }
        public ExecutionStatus Status { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int DurationMs { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorNodeId { get; set; }
        public List<string> ExecutedNodes { get; set; }
    }
}
