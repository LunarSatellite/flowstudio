using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("DataQualityMetricses", Schema = "train")]

    [Index(nameof(CreatedAt))]

    public class DataQualityMetrics : TenantBaseEntity
    {
        public double OverallQuality { get; set; }
        public double Completeness { get; set; }
        public double Consistency { get; set; }
        public double Accuracy { get; set; }
        public int DuplicateCount { get; set; }
        public int MissingLabelCount { get; set; }
        public Dictionary<string, int> ClassDistribution { get; set; } = new();
        public bool IsBalanced { get; set; } = true;
        public Dictionary<string, object> CustomMetrics { get; set; } = new();
    }
}