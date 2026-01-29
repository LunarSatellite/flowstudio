using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.Tenant
{
    /// <summary>
    /// Detailed per-request cost breakdown for complete transparency
    /// Shows exact calculation: Your Cost → Markup → Client Cost → Profit
    /// </summary>
    [Table("UsageBreakdown")]
    public class UsageBreakdown : TenantBaseEntity
    {
        // Tracking references
        [Required]
        public Guid ConversationId { get; set; }
        
        public Guid? MessageId { get; set; }
        
        public Guid? FlowExecutionId { get; set; }
        
        [Required]
        public UsageType Type { get; set; }
        
        // Service details
        [Required]
        [MaxLength(50)]
        public string Provider { get; set; }
        
        [MaxLength(50)]
        public string Model { get; set; }
        
        [MaxLength(50)]
        public string ServiceRegion { get; set; }
        
        // Quantity breakdown
        public int InputQuantity { get; set; }
        
        public int OutputQuantity { get; set; }
        
        public int TotalQuantity { get; set; }
        
        // Provider rates
        [Column(TypeName = "decimal(18,6)")]
        public decimal InputRatePer1K { get; set; }
        
        [Column(TypeName = "decimal(18,6)")]
        public decimal OutputRatePer1K { get; set; }
        
        // Your costs (what YOU pay to provider)
        [Column(TypeName = "decimal(18,6)")]
        public decimal YourInputCost { get; set; }
        
        [Column(TypeName = "decimal(18,6)")]
        public decimal YourOutputCost { get; set; }
        
        [Column(TypeName = "decimal(18,6)")]
        public decimal YourTotalCost { get; set; }
        
        // Client pricing (what THEY pay to you)
        [Column(TypeName = "decimal(18,2)")]
        public decimal MarkupPercent { get; set; }
        
        [Column(TypeName = "decimal(18,6)")]
        public decimal ClientInputCost { get; set; }
        
        [Column(TypeName = "decimal(18,6)")]
        public decimal ClientOutputCost { get; set; }
        
        [Column(TypeName = "decimal(18,6)")]
        public decimal ClientTotalCost { get; set; }
        
        // Profit tracking
        [Column(TypeName = "decimal(18,6)")]
        public decimal ProfitAmount { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal ProfitPercent { get; set; }
        
        // Audit trail
        [Required]
        public DateTime UsedAt { get; set; }
        
        [MaxLength(255)]
        public string UsedBy { get; set; }
        
        [MaxLength(500)]
        public string Notes { get; set; }
        
        // Billing reference
        public Guid? InvoiceId { get; set; }
        
        public bool IsBilled { get; set; }
        
        public DateTime? BilledAt { get; set; }
    }
}
