using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Conversation
{
    [Table("CustomerActivities", Schema = "conv")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class CustomerActivity : TenantBaseEntity
    {
        public Guid CustomerId { get; set; }
        public ActivityType Type { get; set; } = ActivityType.PageView;
        [MaxLength(200)]
        public string? Title { get; set; }
        [MaxLength(4000)]
        public string? Description { get; set; }
        [MaxLength(2000)]
        public string? Url { get; set; }
        [MaxLength(200)]
        public string? Source { get; set; }
        public Dictionary<string, object> Properties { get; set; } = new();
        [Column(TypeName = "datetime2")]
        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Customer Customer { get; set; } = null!;
    }
}