using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Conversation
{
    [Table("CustomerSegmentses", Schema = "conv")]

        public class CustomerSegments
    {
        [MaxLength(200)]
        public string? LifecycleStage { get; set; } // Lead, MQL, SQL, Customer, Evangelist
        [MaxLength(200)]
        public string? ValueSegment { get; set; } // Low, Medium, High, VIP
        [MaxLength(200)]
        public string? EngagementLevel { get; set; } // Cold, Warm, Hot
        public double? LifetimeValue { get; set; }
        public int? PurchaseCount { get; set; }
        public Dictionary<string, string> CustomSegments { get; set; } = new();
    }
}