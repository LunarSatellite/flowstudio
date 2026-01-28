using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Commerce
{
    public class AddToCartRequest
    {
        public Guid ProductId { get; set; }
        public Guid? VariantId { get; set; }
        public int Quantity { get; set; } = 1;
        public Guid? CartId { get; set; }
        public string? SessionId { get; set; }
    }
}