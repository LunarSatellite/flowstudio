using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Commerce
{
    [Table("Products", Schema = "commerce")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class Product : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        [MaxLength(200)]
        public string SKU { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? Barcode { get; set; }
        public ProductStatus Status { get; set; } = ProductStatus.Active;

        // Pricing
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? CompareAtPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Cost { get; set; }
        [MaxLength(200)]
        public string Currency { get; set; } = "USD";
        public bool TaxExempt { get; set; } = false;

        // Inventory
        public int StockQuantity { get; set; }
        public int? LowStockThreshold { get; set; }
        public bool TrackInventory { get; set; } = true;
        public bool AllowBackorder { get; set; } = false;

        // Physical Properties
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Weight { get; set; }
        [MaxLength(200)]
        public string? WeightUnit { get; set; } = "kg";
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Length { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Width { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Height { get; set; }
        [MaxLength(200)]
        public string? DimensionUnit { get; set; } = "cm";

        // Media
        public List<string> ImageUrls { get; set; } = new();
        [MaxLength(2000)]
        public string? PrimaryImageUrl { get; set; }
        [MaxLength(2000)]
        public string? VideoUrl { get; set; }

        // Categorization
        public Guid? CategoryId { get; set; }
        [MaxLength(200)]
        public string? Brand { get; set; }
        public List<string> Tags { get; set; } = new();

        // SEO
        [MaxLength(200)]
        public string? MetaTitle { get; set; }
        [MaxLength(4000)]
        public string? MetaDescription { get; set; }
        [MaxLength(200)]
        public string? Slug { get; set; }

        // Custom Fields
        public Dictionary<string, object> CustomFields { get; set; } = new();
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public ProductCategory? Category { get; set; }
        public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
        public ICollection<ProductReview> Reviews { get; set; } = new List<ProductReview>();
    }
}