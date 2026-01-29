using System;
using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Tenant
{
    /// <summary>
    /// Detailed cost breakdown - shows YOUR cost vs CLIENT cost
    /// </summary>
    public class UsageBreakdownDTO : TenantBaseDTO
    {
        public Guid ConversationId { get; set; }
        public Guid? MessageId { get; set; }
        public Guid? FlowExecutionId { get; set; }
        public UsageType Type { get; set; }
        
        public string Provider { get; set; }
        public string Model { get; set; }
        public string ServiceRegion { get; set; }
        
        public int InputQuantity { get; set; }
        public int OutputQuantity { get; set; }
        public int TotalQuantity { get; set; }
        
        public decimal InputRatePer1K { get; set; }
        public decimal OutputRatePer1K { get; set; }
        
        public decimal YourInputCost { get; set; }
        public decimal YourOutputCost { get; set; }
        public decimal YourTotalCost { get; set; }
        
        public decimal MarkupPercent { get; set; }
        
        public decimal ClientInputCost { get; set; }
        public decimal ClientOutputCost { get; set; }
        public decimal ClientTotalCost { get; set; }
        
        public decimal ProfitAmount { get; set; }
        public decimal ProfitPercent { get; set; }
        
        public DateTime UsedAt { get; set; }
        public string UsedBy { get; set; }
        public string Notes { get; set; }
        
        public Guid? InvoiceId { get; set; }
        public bool IsBilled { get; set; }
        public DateTime? BilledAt { get; set; }
    }
}
