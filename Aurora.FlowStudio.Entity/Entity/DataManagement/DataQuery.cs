using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("DataQueries", Schema = "data")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class DataQuery : TenantBaseEntity
    {
        public Guid DataSourceId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        public QueryType Type { get; set; } = QueryType.Select;

        // Query Definition
        public QueryDefinition Definition { get; set; } = new();

        // Query Optimization
        public bool EnableCache { get; set; } = true;
        public int CacheDuration { get; set; } = 300;

        // Parameters
        public List<QueryParameter> Parameters { get; set; } = new();

        // Result Schema
        public List<ResultColumn> ResultColumns { get; set; } = new();

        // Usage Tracking
        public int ExecutionCount { get; set; } = 0;
        [Column(TypeName = "datetime2")]
        public DateTime? LastExecutedAt { get; set; }
        public double AverageExecutionTime { get; set; }

        // Version Control
        public int Version { get; set; } = 1;
        public bool IsPublished { get; set; } = false;

        public List<string> Tags { get; set; } = new();
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public DataSource DataSource { get; set; } = null!;
        public ICollection<QueryExecution> Executions { get; set; } = new List<QueryExecution>();
    }
}