using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("DeploymentMetricses", Schema = "train")]

    [Index(nameof(CreatedAt))]

    public class DeploymentMetrics : TenantBaseEntity
    {
        public long TotalRequests { get; set; }
        public long SuccessfulRequests { get; set; }
        public long FailedRequests { get; set; }
        public double AverageLatencyMs { get; set; }
        public double P95LatencyMs { get; set; }
        public double P99LatencyMs { get; set; }
        public double Throughput { get; set; }
        public Dictionary<string, object> CustomMetrics { get; set; } = new();
    }
}