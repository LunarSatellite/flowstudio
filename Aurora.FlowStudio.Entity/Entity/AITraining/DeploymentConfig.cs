using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("DeploymentConfigs", Schema = "train")]

    [Index(nameof(CreatedAt))]

    public class DeploymentConfig : TenantBaseEntity
    {
        public int Replicas { get; set; } = 1;
        public bool AutoScaling { get; set; } = false;
        public int? MinReplicas { get; set; }
        public int? MaxReplicas { get; set; }
        public int? TargetCPUUtilization { get; set; }
        public Dictionary<string, object> ResourceLimits { get; set; } = new();
    }
}