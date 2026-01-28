using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Commerce
{
    public class UpdateProductRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public decimal? CompareAtPrice { get; set; }
        public int? StockQuantity { get; set; }
        public ProductStatus? Status { get; set; }
        public List<string>? Tags { get; set; }
    }
}