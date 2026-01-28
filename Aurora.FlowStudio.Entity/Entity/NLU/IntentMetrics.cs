using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.NLU
{
    [Table("IntentMetricses", Schema = "nlu")]

    [Index(nameof(CreatedAt))]

    public class IntentMetrics : TenantBaseEntity
    {
        public int TotalDetections { get; set; }
        public int CorrectDetections { get; set; }
        public int FalsePositives { get; set; }
        public int FalseNegatives { get; set; }
        public double Precision => TotalDetections > 0 ? (double)CorrectDetections / TotalDetections : 0;
        public double Recall => (CorrectDetections + FalseNegatives) > 0 ? (double)CorrectDetections / (CorrectDetections + FalseNegatives) : 0;
        public double F1Score => (Precision + Recall) > 0 ? 2 * (Precision * Recall) / (Precision + Recall) : 0;
        public double AverageConfidence { get; set; }
        public Dictionary<string, object> CustomMetrics { get; set; } = new();
    }
}