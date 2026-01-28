using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Commerce
{
    [Table("OrderItems", Schema = "commerce")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class OrderItem : TenantBaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public Guid? VariantId { get; set; }
        [MaxLength(200)]
        public string ProductName { get; set; } = string.Empty;
        [MaxLength(200)]
        public string SKU { get; set; } = string.Empty;
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; }
        public bool RequiresShipping { get; set; } = true;
        public Dictionary<string, object> Properties { get; set; } = new();
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public Order Order { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public ProductVariant? Variant { get; set; }
    }
}