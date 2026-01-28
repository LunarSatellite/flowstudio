using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Commerce
{
    public class PaymentResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string? TransactionId { get; set; }
        public string? AuthorizationCode { get; set; }
        public string? ErrorMessage { get; set; }
        public PaymentStatus Status { get; set; }
    }
}