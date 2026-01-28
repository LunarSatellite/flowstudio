using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Commerce
{
    public class ApplyDiscountRequest
    {
        public Guid CartId { get; set; }
        public string CouponCode { get; set; } = string.Empty;
    }
}