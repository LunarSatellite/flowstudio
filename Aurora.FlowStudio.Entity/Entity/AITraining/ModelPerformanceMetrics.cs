using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("ModelPerformanceMetricses", Schema = "train")]

    [Index(nameof(CreatedAt))]

    public class ModelPerformanceMetrics : TenantBaseEntity
    {
        public double Accuracy { get; set; }
        public double Precision { get; set; }
        public double Recall { get; set; }
        public double F1Score { get; set; }
        public double AveragePredictionTimeMs { get; set; }
        public Dictionary<string, ClassMetrics> PerClassMetrics { get; set; } = new();
        public Dictionary<string, object> CustomMetrics { get; set; } = new();
    }
}