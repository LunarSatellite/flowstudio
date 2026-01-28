using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Commerce
{
    [Table("ProductCategories", Schema = "commerce")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class ProductCategory : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        [MaxLength(200)]
        public string? Slug { get; set; }
        public Guid? ParentCategoryId { get; set; }
        [MaxLength(2000)]
        public string? ImageUrl { get; set; }
        public int Order { get; set; } = 0;
        public bool IsVisible { get; set; } = true;
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public ProductCategory? ParentCategory { get; set; }
        public ICollection<ProductCategory> SubCategories { get; set; } = new List<ProductCategory>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}