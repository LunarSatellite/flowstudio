using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Integration
{
    [Table("ConnectorEndpoints", Schema = "integration")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class ConnectorEndpoint : TenantBaseEntity
    {
        public Guid ConnectorId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string DisplayName { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        public EndpointType Type { get; set; } = EndpointType.Query;

        // HTTP Configuration
        public HttpMethodType Method { get; set; } = HttpMethodType.GET;
        [MaxLength(2000)]
        public string Path { get; set; } = string.Empty;

        // Database Configuration
        [MaxLength(200)]
        public string? Query { get; set; }
        public QueryType? DatabaseQueryType { get; set; }

        // Request/Response Configuration
        public Dictionary<string, ParameterDefinition> Parameters { get; set; } = new();
        public Dictionary<string, string> Headers { get; set; } = new();
        public RequestBodyConfig? RequestBody { get; set; }
        public ResponseConfig Response { get; set; } = new();

        // Caching
        public CacheConfig? CacheSettings { get; set; }

        // Transformation
        public bool EnableRequestTransformation { get; set; } = false;
        [MaxLength(200)]
        public string? RequestTransformScript { get; set; }
        public bool EnableResponseTransformation { get; set; } = false;
        [MaxLength(200)]
        public string? ResponseTransformScript { get; set; }

        // Validation
        public bool ValidateRequest { get; set; } = true;
        public bool ValidateResponse { get; set; } = true;
        [MaxLength(200)]
        public string? RequestSchema { get; set; }
        [MaxLength(200)]
        public string? ResponseSchema { get; set; }

        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public Connector Connector { get; set; } = null!;
    }
}