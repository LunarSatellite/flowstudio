using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Commerce
{
    [Table("Discounts", Schema = "commerce")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class Discount : TenantBaseEntity
    {
        [MaxLength(100)]
        public string Code { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        public DiscountType Type { get; set; } = DiscountType.Percentage;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? MinimumPurchaseAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? MaximumDiscountAmount { get; set; }
        public int? UsageLimit { get; set; }
        public int UsageCount { get; set; } = 0;
        public bool OnePerCustomer { get; set; } = false;
        [Column(TypeName = "datetime2")]
        public DateTime? StartsAt { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? EndsAt { get; set; }
        public DiscountStatus Status { get; set; } = DiscountStatus.Active;
        public List<Guid> ApplicableProductIds { get; set; } = new();
        public List<Guid> ApplicableCategoryIds { get; set; } = new();
        public Dictionary<string, object> Metadata { get; set; } = new();
    }
}