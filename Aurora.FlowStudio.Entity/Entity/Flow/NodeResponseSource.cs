using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Flow
{
    [Table("NodeResponseSources", Schema = "flow")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class NodeResponseSource : TenantBaseEntity
    {
        public Guid NodeId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        public ResponseSourceType SourceType { get; set; } = ResponseSourceType.Static;
        public int Priority { get; set; } = 0; // For multiple sources, execute in order

        // Static Response
        [MaxLength(200)]
        public string? StaticResponse { get; set; }
        public ResponseFormat ResponseFormat { get; set; } = ResponseFormat.Text;

        // Dynamic Source (API/Database)
        public Guid? ConnectorId { get; set; }
        public Guid? EndpointId { get; set; }
        public Dictionary<string, object> RequestParameters { get; set; } = new();
        [MaxLength(200)]
        public string? QueryTemplate { get; set; } // For database queries

        // Response Processing
        public bool ProcessWithLLM { get; set; } = false;
        [MaxLength(200)]
        public string? LLMPromptTemplate { get; set; }
        public Guid? AIProviderId { get; set; }
        public LLMProcessingConfig? LLMConfig { get; set; }

        // Response Transformation
        public bool EnableTransformation { get; set; } = false;
        [MaxLength(200)]
        public string? TransformationScript { get; set; } // JavaScript/JSONata
        [MaxLength(2000)]
        public string? ResponsePath { get; set; } // JSONPath to extract data

        // Caching
        public bool EnableCaching { get; set; } = false;
        public int CacheDurationSeconds { get; set; } = 300;
        public List<string> CacheKeys { get; set; } = new();

        // Fallback
        [MaxLength(200)]
        public string? FallbackResponse { get; set; }
        public bool RetryOnFailure { get; set; } = true;
        public int MaxRetries { get; set; } = 3;

        // Response Metadata (sent to client)
        public Dictionary<string, object> ClientMetadata { get; set; } = new(); // Sent to client app
        public bool IncludeSourceInfo { get; set; } = false; // Tell client where data came from

        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public FlowNode Node { get; set; } = null!;
        public Integration.Connector? Connector { get; set; }
        public Integration.ConnectorEndpoint? Endpoint { get; set; }
        public AI.AIProvider? AIProvider { get; set; }
    }
}