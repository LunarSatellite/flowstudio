using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AI
{
    [Table("PricingConfigs", Schema = "ai")]

        public class PricingConfig
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal InputCostPer1KTokens { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal OutputCostPer1KTokens { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? CostPerMinute { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? CostPerRequest { get; set; }
        [MaxLength(200)]
        public string Currency { get; set; } = "USD";
    }
}