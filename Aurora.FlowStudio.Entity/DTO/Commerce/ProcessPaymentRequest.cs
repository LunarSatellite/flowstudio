using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Commerce
{
    public class ProcessPaymentRequest
    {
        public Guid OrderId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";
        public Dictionary<string, string> PaymentDetails { get; set; } = new();
    }
}