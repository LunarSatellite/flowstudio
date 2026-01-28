using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Commerce
{
    public class ProductCategoryDTO : BaseDTO
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Slug { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public string? ParentCategoryName { get; set; }
        public string? ImageUrl { get; set; }
        public int Order { get; set; }
        public int ProductCount { get; set; }
    }
}