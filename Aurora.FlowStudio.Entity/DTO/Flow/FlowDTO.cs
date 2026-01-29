using System;
using System.Collections.Generic;
using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Flow
{
    public class FlowDTO : TenantBaseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public FlowCategory Category { get; set; }
        public FlowStatus Status { get; set; }
        public string TriggerType { get; set; }
        public string TriggerValue { get; set; }
        public DateTime? PublishedAt { get; set; }
        public Guid? PublishedBy { get; set; }
        public int ExecutionCount { get; set; }
        public int SuccessCount { get; set; }
        public int FailureCount { get; set; }
        public decimal SuccessRate { get; set; }
        public decimal AverageDurationSeconds { get; set; }
        public List<string> Tags { get; set; }
    }
}
