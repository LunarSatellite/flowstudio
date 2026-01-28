using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

using Aurora.FlowStudio.Entity.Entity.Commerce;

namespace Aurora.FlowStudio.Entity.Entity.Messaging
{
    [Table("ReceiptConfigs", Schema = "msg")]

    [Index(nameof(CreatedAt))]

    public class ReceiptConfig : TenantBaseEntity
    {
        [MaxLength(200)]
        public string OrderNumber { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Currency { get; set; } = "USD";
        [MaxLength(200)]
        public string PaymentMethod { get; set; } = string.Empty;
        [Column(TypeName = "datetime2")]
        public DateTime Timestamp { get; set; }
        public List<ReceiptItem> Items { get; set; } = new();
        public Address? ShippingAddress { get; set; }
        public ReceiptSummary Summary { get; set; } = new();
    }
}