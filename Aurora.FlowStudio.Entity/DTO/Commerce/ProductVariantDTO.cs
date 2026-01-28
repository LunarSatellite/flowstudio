using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Commerce
{
    public class ProductVariantDTO : BaseDTO
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public Dictionary<string, string> Options { get; set; } = new();
        public decimal? Price { get; set; }
        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsDefault { get; set; }
    }
}