using System;
using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Tenant
{
    public class UsageDTO : TenantBaseDTO
    {
        public UsageType Type { get; set; }
        public string Provider { get; set; }
        public string Model { get; set; }
        public int Quantity { get; set; }
        public decimal BaseCost { get; set; }
        public decimal MarkupPercent { get; set; }
        public decimal CustomerCost { get; set; }
        public Guid? ConversationId { get; set; }
        public Guid? FlowExecutionId { get; set; }
        public DateTime UsedAt { get; set; }
        public string Region { get; set; }
    }
}
