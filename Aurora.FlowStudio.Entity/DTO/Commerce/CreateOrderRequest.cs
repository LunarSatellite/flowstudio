using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Commerce
{
    public class CreateOrderRequest
    {
        public Guid CartId { get; set; }
        public Guid? CustomerId { get; set; }
        public string CustomerEmail { get; set; } = string.Empty;
        public string? CustomerPhone { get; set; }
        public string? CustomerName { get; set; }
        public AddressDTO ShippingAddress { get; set; } = new();
        public AddressDTO BillingAddress { get; set; } = new();
        public PaymentMethod PaymentMethod { get; set; }
        public string? CustomerNote { get; set; }
    }
}