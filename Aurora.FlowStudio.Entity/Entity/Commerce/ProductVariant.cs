using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Commerce
{
    [Table("ProductVariants", Schema = "commerce")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class ProductVariant : TenantBaseEntity
    {
        public Guid ProductId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string SKU { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? Barcode { get; set; }

        // Variant Options
        public Dictionary<string, string> Options { get; set; } = new(); // e.g., {"Size": "Large", "Color": "Red"}

        // Pricing (can override product price)
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? CompareAtPrice { get; set; }

        // Inventory
        public int StockQuantity { get; set; }
        public bool TrackInventory { get; set; } = true;

        // Media
        [MaxLength(2000)]
        public string? ImageUrl { get; set; }

        // Physical Properties
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Weight { get; set; }

        public bool IsDefault { get; set; } = false;
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public Product Product { get; set; } = null!;
    }
}