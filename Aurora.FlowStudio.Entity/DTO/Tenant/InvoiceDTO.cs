using System;
using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Tenant
{
    public class InvoiceDTO : TenantBaseDTO
    {
        public string InvoiceNumber { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        
        public decimal SubscriptionFee { get; set; }
        public decimal TokenCost { get; set; }
        public decimal VoiceCost { get; set; }
        public decimal SMSCost { get; set; }
        public decimal EmailCost { get; set; }
        public decimal APICost { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal FinalAmount { get; set; }
        
        public InvoiceStatus Status { get; set; }
        public DateTime? PaidAt { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentTransactionId { get; set; }
    }
}
