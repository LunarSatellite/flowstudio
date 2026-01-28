using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Commerce
{
    [Table("OrderTransactions", Schema = "commerce")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class OrderTransaction : TenantBaseEntity
    {
        public Guid OrderId { get; set; }
        public TransactionType Type { get; set; } = TransactionType.Payment;
        public TransactionStatus Status { get; set; } = TransactionStatus.Pending;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [MaxLength(200)]
        public string Currency { get; set; } = "USD";
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.CreditCard;
        [MaxLength(200)]
        public string? PaymentGateway { get; set; }
        [MaxLength(100)]
        public string? TransactionId { get; set; }
        [MaxLength(100)]
        public string? AuthorizationCode { get; set; }
        [MaxLength(4000)]
        public string? ErrorMessage { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? ProcessedAt { get; set; }
        public Dictionary<string, object> GatewayResponse { get; set; } = new();
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public Order Order { get; set; } = null!;
    }
}