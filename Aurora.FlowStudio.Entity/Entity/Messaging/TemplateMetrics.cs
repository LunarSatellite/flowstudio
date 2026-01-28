using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Messaging
{
    [Table("TemplateMetricses", Schema = "msg")]

    [Index(nameof(CreatedAt))]

    public class TemplateMetrics : TenantBaseEntity
    {
        public int TotalUsage { get; set; }
        public int SuccessfulDelivery { get; set; }
        public int FailedDelivery { get; set; }
        public double AverageResponseTime { get; set; }
        public double EngagementRate { get; set; }
        public double SatisfactionScore { get; set; }
        public Dictionary<string, int> UsageByChannel { get; set; } = new();
        public Dictionary<string, int> UsageByLanguage { get; set; } = new();
        public Dictionary<string, object> CustomMetrics { get; set; } = new();
    }
}