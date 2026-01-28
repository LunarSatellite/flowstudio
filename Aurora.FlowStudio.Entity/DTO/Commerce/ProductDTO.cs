using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Commerce
{
    public class ProductDTO : BaseDTO
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string SKU { get; set; } = string.Empty;
        public ProductStatus Status { get; set; }
        public decimal Price { get; set; }
        public decimal? CompareAtPrice { get; set; }
        public string Currency { get; set; } = "USD";
        public int StockQuantity { get; set; }
        public bool TrackInventory { get; set; }
        public List<string> ImageUrls { get; set; } = new();
        public string? PrimaryImageUrl { get; set; }
        public Guid? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? Brand { get; set; }
        public List<string> Tags { get; set; } = new();
        public List<ProductVariantDTO> Variants { get; set; } = new();
        public double? AverageRating { get; set; }
        public int ReviewCount { get; set; }
    }
}