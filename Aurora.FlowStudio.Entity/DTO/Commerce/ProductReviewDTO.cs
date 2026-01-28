using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Commerce
{
    public class ProductReviewDTO : BaseDTO
    {
        public Guid ProductId { get; set; }
        public Guid? CustomerId { get; set; }
        public string? ReviewerName { get; set; }
        public int Rating { get; set; }
        public string? Title { get; set; }
        public string? Comment { get; set; }
        public ReviewStatus Status { get; set; }
        public bool IsVerifiedPurchase { get; set; }
        public int HelpfulCount { get; set; }
        public DateTime? PublishedAt { get; set; }
    }
}