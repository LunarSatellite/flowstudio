using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Messaging
{
    [Table("ReceiptSummaries", Schema = "msg")]

    [Index(nameof(CreatedAt))]

    public class ReceiptSummary : TenantBaseEntity
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ShippingCost { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Tax { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
    }
}