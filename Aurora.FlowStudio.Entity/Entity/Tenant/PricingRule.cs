using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;

namespace Aurora.FlowStudio.Entity.Tenant
{
    /// <summary>
    /// Your markup configuration per service
    /// </summary>
    [Table("PricingRules")]
    public class PricingRule : TenantBaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string ServiceType { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Provider { get; set; }
        
        [MaxLength(50)]
        public string Model { get; set; }
        
        [Column(TypeName = "decimal(18,6)")]
        public decimal BaseCostPer1K { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal MarkupPercent { get; set; }
        
        [Column(TypeName = "decimal(18,6)")]
        public decimal FinalPricePer1K { get; set; }
        
        public bool IsActive { get; set; }
        
        [Required]
        public DateTime EffectiveFrom { get; set; }
        
        public DateTime? EffectiveTo { get; set; }
    }
}
