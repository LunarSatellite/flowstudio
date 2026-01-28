using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Flow
{
    [Table("FlowConnections", Schema = "flow")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class FlowConnection : TenantBaseEntity
    {
        public Guid FlowId { get; set; }
        public Guid SourceNodeId { get; set; }
        public Guid TargetNodeId { get; set; }
        [MaxLength(200)]
        public string? Label { get; set; }
        public ConnectionType Type { get; set; } = ConnectionType.Default;
        public Dictionary<string, object> Condition { get; set; } = new();
        public int Priority { get; set; } = 0;
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public Flow Flow { get; set; } = null!;
        public FlowNode SourceNode { get; set; } = null!;
        public FlowNode TargetNode { get; set; } = null!;
    }
}