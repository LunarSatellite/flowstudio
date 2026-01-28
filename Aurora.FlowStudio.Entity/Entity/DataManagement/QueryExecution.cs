using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("QueryExecutions", Schema = "data")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class QueryExecution : TenantBaseEntity
    {
        public Guid QueryId { get; set; }
        public Guid? UserId { get; set; }
        public Dictionary<string, object> Parameters { get; set; } = new();
        public ExecutionStatus Status { get; set; } = ExecutionStatus.Success;
        public int RowsReturned { get; set; }
        public int ExecutionTimeMs { get; set; }
        [MaxLength(4000)]
        public string? ErrorMessage { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime ExecutedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public DataQuery Query { get; set; } = null!;
    }
}