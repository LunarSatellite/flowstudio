using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Commerce
{
    public class DiscountDTO : BaseDTO
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DiscountType Type { get; set; }
        public decimal Value { get; set; }
        public decimal? MinimumPurchaseAmount { get; set; }
        public decimal? MaximumDiscountAmount { get; set; }
        public int? UsageLimit { get; set; }
        public int UsageCount { get; set; }
        public DateTime? StartsAt { get; set; }
        public DateTime? EndsAt { get; set; }
        public DiscountStatus Status { get; set; }
    }
}