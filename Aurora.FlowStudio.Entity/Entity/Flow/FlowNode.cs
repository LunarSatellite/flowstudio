using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Flow
{
    [Table("FlowNodes", Schema = "flow")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class FlowNode : TenantBaseEntity
    {
        public Guid FlowId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Label { get; set; }
        public NodeType Type { get; set; } = NodeType.Message;
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public Dictionary<string, object> Configuration { get; set; } = new();
        public Dictionary<string, object> Data { get; set; } = new();
        [MaxLength(100)]
        public string? ParentNodeId { get; set; }
        public int Order { get; set; }
        public bool IsEntryPoint { get; set; } = false;
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public Flow Flow { get; set; } = null!;
        public ICollection<FlowConnection> OutgoingConnections { get; set; } = new List<FlowConnection>();
        public ICollection<FlowConnection> IncomingConnections { get; set; } = new List<FlowConnection>();
        public ICollection<NodeAction> Actions { get; set; } = new List<NodeAction>();
    }
}