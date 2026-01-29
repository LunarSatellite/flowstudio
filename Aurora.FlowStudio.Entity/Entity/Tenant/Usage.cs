using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.Tenant
{
    /// <summary>
    /// High-level usage tracking (aggregated)
    /// For detailed breakdown, see UsageBreakdown
    /// </summary>
    [Table("Usage")]
    public class Usage : TenantBaseEntity
    {
        [Required]
        public UsageType Type { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Provider { get; set; }
        
        [MaxLength(50)]
        public string Model { get; set; }
        
        public int Quantity { get; set; }
        
        [Column(TypeName = "decimal(18,6)")]
        public decimal BaseCost { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal MarkupPercent { get; set; }
        
        [Column(TypeName = "decimal(18,6)")]
        public decimal CustomerCost { get; set; }
        
        public Guid? ConversationId { get; set; }
        
        public Guid? FlowExecutionId { get; set; }
        
        [Required]
        public DateTime UsedAt { get; set; }
        
        [MaxLength(50)]
        public string Region { get; set; }
    }
}
