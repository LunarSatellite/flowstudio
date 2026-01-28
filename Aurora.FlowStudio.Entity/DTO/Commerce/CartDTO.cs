using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Commerce
{
    public class CartDTO : BaseDTO
    {
        public Guid? CustomerId { get; set; }
        public Guid? ConversationId { get; set; }
        public string? SessionId { get; set; }
        public CartStatus Status { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal ShippingAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; } = "USD";
        public string? CouponCode { get; set; }
        public int ItemCount { get; set; }
        public List<CartItemDTO> Items { get; set; } = new();
    }
}