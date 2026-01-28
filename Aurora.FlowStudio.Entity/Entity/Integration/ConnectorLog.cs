using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Integration
{
    [Table("ConnectorLogs", Schema = "integration")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class ConnectorLog : TenantBaseEntity
    {
        public Guid ConnectorId { get; set; }
        public Guid? EndpointId { get; set; }
        public Guid? ConversationId { get; set; }
        public Guid? NodeId { get; set; }
        public LogLevel Level { get; set; } = LogLevel.Info;
        [MaxLength(4000)]
        public string Message { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? RequestData { get; set; }
        [MaxLength(200)]
        public string? ResponseData { get; set; }
        public int? StatusCode { get; set; }
        public int DurationMs { get; set; }
        public bool IsSuccess { get; set; } = true;
        [MaxLength(4000)]
        public string? ErrorMessage { get; set; }
        [MaxLength(200)]
        public string? StackTrace { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public Connector Connector { get; set; } = null!;
        public ConnectorEndpoint? Endpoint { get; set; }
    }
}