using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Commerce
{
    [Table("Carts", Schema = "commerce")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class Cart : TenantBaseEntity
    {
        public Guid? CustomerId { get; set; }
        public Guid? ConversationId { get; set; }
        [MaxLength(100)]
        public string? SessionId { get; set; }
        public CartStatus Status { get; set; } = CartStatus.Active;

        // Totals
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

        // Discount
        [MaxLength(100)]
        public string? CouponCode { get; set; }
        public Guid? DiscountId { get; set; }

        // Expiry
        [Column(TypeName = "datetime2")]
        public DateTime? ExpiresAt { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? ConvertedToOrderAt { get; set; }

        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public Conversation.Customer? Customer { get; set; }
        public Conversation.Conversation? Conversation { get; set; }
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}