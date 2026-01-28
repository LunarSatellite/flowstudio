using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Commerce
{
    [Table("Orders", Schema = "commerce")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class Order : TenantBaseEntity
    {
        [MaxLength(200)]
        public string OrderNumber { get; set; } = string.Empty;
        public Guid? CustomerId { get; set; }
        public Guid? ConversationId { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public FulfillmentStatus FulfillmentStatus { get; set; } = FulfillmentStatus.Unfulfilled;

        // Amounts
        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ShippingAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        [MaxLength(200)]
        public string Currency { get; set; } = "USD";

        // Customer Info
        [MaxLength(255)]
        public string CustomerEmail { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? CustomerPhone { get; set; }
        [MaxLength(200)]
        public string? CustomerName { get; set; }

        // Addresses
        public Address ShippingAddress { get; set; } = new();
        public Address BillingAddress { get; set; } = new();

        // Dates
        [Column(TypeName = "datetime2")]
        public DateTime? PaidAt { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? FulfilledAt { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? CancelledAt { get; set; }
        [MaxLength(200)]
        public string? CancellationReason { get; set; }

        // Notes
        [MaxLength(200)]
        public string? CustomerNote { get; set; }
        [MaxLength(200)]
        public string? InternalNote { get; set; }

        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public Conversation.Customer? Customer { get; set; }
        public Conversation.Conversation? Conversation { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        public ICollection<OrderTransaction> Transactions { get; set; } = new List<OrderTransaction>();
    }
}