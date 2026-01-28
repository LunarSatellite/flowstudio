using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Integration
{
    [Table("Connectors", Schema = "integration")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class Connector : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string DisplayName { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        public ConnectorType Type { get; set; } = ConnectorType.RestAPI;
        public ConnectorCategory Category { get; set; } = ConnectorCategory.Custom;
        [MaxLength(2000)]
        public string? IconUrl { get; set; }
        public ConnectorStatus Status { get; set; } = ConnectorStatus.Active;

        // Connection Details
        public ConnectionConfig Configuration { get; set; } = new();

        // Authentication
        public AuthenticationConfig Authentication { get; set; } = new();

        // Rate Limiting
        public RateLimitConfig? RateLimit { get; set; }

        // Retry & Timeout
        public RetryConfig RetryPolicy { get; set; } = new();
        public int TimeoutSeconds { get; set; } = 30;

        // Health Check
        [MaxLength(200)]
        public string? HealthCheckEndpoint { get; set; }
        public int HealthCheckIntervalMinutes { get; set; } = 5;
        [Column(TypeName = "datetime2")]
        public DateTime? LastHealthCheckAt { get; set; }
        public HealthStatus? LastHealthStatus { get; set; }

        // Versioning
        [MaxLength(200)]
        public string? ApiVersion { get; set; }
        public bool IsVersioned { get; set; } = false;

        // Security
        public bool IsEncrypted { get; set; } = true;
        public bool AllowInsecure { get; set; } = false;
        public List<string> AllowedIpAddresses { get; set; } = new();

        // Monitoring
        public ConnectorMetrics Metrics { get; set; } = new();
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public ICollection<ConnectorEndpoint> Endpoints { get; set; } = new List<ConnectorEndpoint>();
        public ICollection<ConnectorLog> Logs { get; set; } = new List<ConnectorLog>();
        public ICollection<Flow.FlowIntegration> FlowIntegrations { get; set; } = new List<Flow.FlowIntegration>();
    }
}