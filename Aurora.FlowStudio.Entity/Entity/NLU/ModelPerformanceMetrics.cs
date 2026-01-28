using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.NLU
{
    [Table("ModelPerformanceMetricses", Schema = "nlu")]

    [Index(nameof(CreatedAt))]

    public class ModelPerformanceMetrics : TenantBaseEntity
    {
        public double OverallAccuracy { get; set; }
        public double Precision { get; set; }
        public double Recall { get; set; }
        public double F1Score { get; set; }
        public double AverageConfidence { get; set; }
        public Dictionary<string, IntentPerformance> IntentPerformance { get; set; } = new();
        public Dictionary<string, EntityPerformance> EntityPerformance { get; set; } = new();
        public Dictionary<string, object> CustomMetrics { get; set; } = new();
    }
}