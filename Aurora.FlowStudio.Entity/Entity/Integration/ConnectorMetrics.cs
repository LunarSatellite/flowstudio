using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Integration
{
    [Table("ConnectorMetricses", Schema = "integration")]

        public class ConnectorMetrics
    {
        public long TotalRequests { get; set; }
        public long SuccessfulRequests { get; set; }
        public long FailedRequests { get; set; }
        public double AverageResponseTimeMs { get; set; }
        public double SuccessRate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? LastRequestAt { get; set; }
        public Dictionary<string, long> EndpointUsage { get; set; } = new();
        public Dictionary<string, object> CustomMetrics { get; set; } = new();
    }
}