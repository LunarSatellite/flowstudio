using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Flow
{
    [Table("NodeActions", Schema = "flow")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class NodeAction : TenantBaseEntity
    {
        public Guid NodeId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        public ActionType Type { get; set; } = ActionType.Custom;
        public Dictionary<string, object> Configuration { get; set; } = new();
        public int Order { get; set; }
        public bool IsAsync { get; set; } = false;
        public int? TimeoutSeconds { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public FlowNode Node { get; set; } = null!;
    }
}