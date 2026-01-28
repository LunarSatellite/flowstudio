using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Flow
{
    [Table("FlowAnalyticses", Schema = "flow")]

        public class FlowAnalytics
    {
        public int TotalConversations { get; set; }
        public int CompletedConversations { get; set; }
        public int AbandonedConversations { get; set; }
        public double AverageCompletionTime { get; set; }
        public double CompletionRate { get; set; }
        public double AverageSatisfactionScore { get; set; }
        public Dictionary<string, int> NodeVisits { get; set; } = new();
        public Dictionary<string, object> CustomMetrics { get; set; } = new();
    }
}