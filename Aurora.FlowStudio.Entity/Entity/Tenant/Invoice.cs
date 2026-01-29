using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.Tenant
{
    /// <summary>
    /// Monthly invoice with complete breakdown
    /// </summary>
    [Table("Invoices")]
    public class Invoice : TenantBaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string InvoiceNumber { get; set; }
        
        [Required]
        public DateTime PeriodStart { get; set; }
        
        [Required]
        public DateTime PeriodEnd { get; set; }
        
        // Cost breakdown
        [Column(TypeName = "decimal(18,2)")]
        public decimal SubscriptionFee { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TokenCost { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal VoiceCost { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal SMSCost { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal EmailCost { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal APICost { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxAmount { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal FinalAmount { get; set; }
        
        [Required]
        public InvoiceStatus Status { get; set; }
        
        public DateTime? PaidAt { get; set; }
        
        [MaxLength(100)]
        public string PaymentMethod { get; set; }
        
        [MaxLength(255)]
        public string PaymentTransactionId { get; set; }
    }
}
