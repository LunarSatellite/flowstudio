using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Messaging
{
    [Table("VariantMetricses", Schema = "msg")]

    [Index(nameof(CreatedAt))]

    public class VariantMetrics : TenantBaseEntity
    {
        public int Impressions { get; set; }
        public int Clicks { get; set; }
        public int Conversions { get; set; }
        public double ClickThroughRate => Impressions > 0 ? (double)Clicks / Impressions : 0;
        public double ConversionRate => Clicks > 0 ? (double)Conversions / Clicks : 0;
        public double AverageResponseTime { get; set; }
        public double SatisfactionScore { get; set; }
    }
}