using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("DataSources", Schema = "data")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
    [Index(nameof(CreatedAt))]

    public class DataSource : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string DisplayName { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        public DataSourceType Type { get; set; } = DataSourceType.Database;
        public DataSourceStatus Status { get; set; } = DataSourceStatus.Active;

        // Connection
        public Guid? ConnectorId { get; set; }
        public DataSourceConnection Connection { get; set; } = new();

        // Schema Discovery
        public bool AutoDiscoverSchema { get; set; } = true;
        [Column(TypeName = "datetime2")]
        public DateTime? LastSchemaDiscovery { get; set; }
        public DataSourceSchema Schema { get; set; } = new();

        // Data Refresh
        public RefreshStrategy RefreshStrategy { get; set; } = RefreshStrategy.OnDemand;
        public RefreshSchedule? Schedule { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? LastRefreshAt { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? NextRefreshAt { get; set; }

        // Caching
        public CachingStrategy Caching { get; set; } = new();

        // Query Configuration
        public QueryConfig DefaultQueryConfig { get; set; } = new();

        // Data Transformation
        public bool EnableTransformation { get; set; } = false;
        public List<DataTransformation> Transformations { get; set; } = new();

        // Data Quality
        public DataQualityRules? QualityRules { get; set; }

        // Access Control
        public DataAccessPolicy AccessPolicy { get; set; } = new();

        // Monitoring
        public DataSourceMetrics Metrics { get; set; } = new();

        // Tags
        public List<string> Tags { get; set; } = new();
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public Integration.Connector? Connector { get; set; }
        public ICollection<DataQuery> Queries { get; set; } = new List<DataQuery>();
        public ICollection<DataSourceLog> Logs { get; set; } = new List<DataSourceLog>();
    }
}