using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Commerce
{
    public class OrderDTO : BaseDTO
    {
        public string OrderNumber { get; set; } = string.Empty;
        public Guid? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string CustomerEmail { get; set; } = string.Empty;
        public OrderStatus Status { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public FulfillmentStatus FulfillmentStatus { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal ShippingAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; } = "USD";
        public AddressDTO ShippingAddress { get; set; } = new();
        public AddressDTO BillingAddress { get; set; } = new();
        public DateTime? PaidAt { get; set; }
        public DateTime? FulfilledAt { get; set; }
        public List<OrderItemDTO> Items { get; set; } = new();
    }
}