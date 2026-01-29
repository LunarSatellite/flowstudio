using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;

namespace Aurora.FlowStudio.Entity.Tenant
{
    /// <summary>
    /// Complete audit trail of all pricing changes
    /// Ensures transparency when rates change
    /// </summary>
    [Table("PricingHistory")]
    public class PricingHistory : BaseEntity
    {
        public Guid? TenantId { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string ServiceType { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Provider { get; set; }
        
        [MaxLength(50)]
        public string Model { get; set; }
        
        // Old pricing
        [Column(TypeName = "decimal(18,6)")]
        public decimal OldBaseCost { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal OldMarkup { get; set; }
        
        [Column(TypeName = "decimal(18,6)")]
        public decimal OldClientPrice { get; set; }
        
        // New pricing
        [Column(TypeName = "decimal(18,6)")]
        public decimal NewBaseCost { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal NewMarkup { get; set; }
        
        [Column(TypeName = "decimal(18,6)")]
        public decimal NewClientPrice { get; set; }
        
        // Change details
        [Column(TypeName = "decimal(18,2)")]
        public decimal ChangePercent { get; set; }
        
        [MaxLength(500)]
        public string ChangeReason { get; set; }
        
        [Required]
        public DateTime EffectiveFrom { get; set; }
        
        [Required]
        public Guid ChangedBy { get; set; }
        
        // Notification tracking
        public bool ClientNotified { get; set; }
        
        public DateTime? NotifiedAt { get; set; }
    }
}
