using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("DataSourceMetricses", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class DataSourceMetrics : TenantBaseEntity
    {
        public long TotalQueries { get; set; }
        public long SuccessfulQueries { get; set; }
        public long FailedQueries { get; set; }
        public double AverageQueryTime { get; set; }
        public double CacheHitRate { get; set; }
        public long TotalBytesTransferred { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? LastAccessedAt { get; set; }
        public Dictionary<string, object> CustomMetrics { get; set; } = new();
    }
}