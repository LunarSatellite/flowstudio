using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Flow
{
    [Table("FlowIntegrations", Schema = "flow")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class FlowIntegration : TenantBaseEntity
    {
        public Guid FlowId { get; set; }
        public Guid ConnectorId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        public IntegrationType Type { get; set; } = IntegrationType.API;
        public bool IsActive { get; set; } = true;

        // Configuration
        public Dictionary<string, object> Configuration { get; set; } = new();
        public Dictionary<string, string> FieldMappings { get; set; } = new();

        // Trigger Configuration
        public IntegrationTrigger Trigger { get; set; } = IntegrationTrigger.Manual;
        public List<string> TriggerEvents { get; set; } = new();

        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public Flow Flow { get; set; } = null!;
        public Integration.Connector Connector { get; set; } = null!;
    }
}