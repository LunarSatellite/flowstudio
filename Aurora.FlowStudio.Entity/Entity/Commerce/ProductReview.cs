using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Commerce
{
    [Table("ProductReviews", Schema = "commerce")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class ProductReview : TenantBaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid? CustomerId { get; set; }
        [MaxLength(200)]
        public string? ReviewerName { get; set; }
        [MaxLength(255)]
        public string? ReviewerEmail { get; set; }
        public int Rating { get; set; } // 1-5
        [MaxLength(200)]
        public string? Title { get; set; }
        [MaxLength(200)]
        public string? Comment { get; set; }
        public ReviewStatus Status { get; set; } = ReviewStatus.Pending;
        public bool IsVerifiedPurchase { get; set; } = false;
        public int HelpfulCount { get; set; } = 0;
        public int UnhelpfulCount { get; set; } = 0;
        [Column(TypeName = "datetime2")]
        public DateTime? PublishedAt { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public Product Product { get; set; } = null!;
        public Conversation.Customer? Customer { get; set; }
    }
}