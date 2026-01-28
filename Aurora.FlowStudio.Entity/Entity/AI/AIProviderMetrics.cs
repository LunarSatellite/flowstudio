using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AI
{
    [Table("AIProviderMetricses", Schema = "ai")]

        public class AIProviderMetrics
    {
        public long TotalRequests { get; set; }
        public long SuccessfulRequests { get; set; }
        public long FailedRequests { get; set; }
        public long TotalTokensUsed { get; set; }
        public long TotalInputTokens { get; set; }
        public long TotalOutputTokens { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalCost { get; set; }
        public double AverageLatencyMs { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? LastRequestAt { get; set; }
    }
}