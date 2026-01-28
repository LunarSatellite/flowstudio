using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("ModelDeployments", Schema = "train")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class ModelDeployment : TenantBaseEntity
    {
        public Guid ModelRegistryId { get; set; }
        [MaxLength(200)]
        public string DeploymentName { get; set; } = string.Empty;
        public DeploymentEnvironment Environment { get; set; } = DeploymentEnvironment.Development;
        public DeploymentStatus Status { get; set; } = DeploymentStatus.Active;
        [MaxLength(200)]
        public string? Endpoint { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime DeployedAt { get; set; } = DateTime.UtcNow;
        [Column(TypeName = "datetime2")]
        public DateTime? UndeployedAt { get; set; }
        public DeploymentConfig Configuration { get; set; } = new();
        public DeploymentMetrics Metrics { get; set; } = new();

        // Navigation properties
        public ModelRegistry ModelRegistry { get; set; } = null!;
    }
}