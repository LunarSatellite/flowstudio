using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("DataSourceLogs", Schema = "data")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class DataSourceLog : TenantBaseEntity
    {
        public Guid DataSourceId { get; set; }
        public LogLevel Level { get; set; } = LogLevel.Info;
        [MaxLength(200)]
        public string Event { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Message { get; set; }
        public Dictionary<string, object> Data { get; set; } = new();

        // Navigation properties
        public DataSource DataSource { get; set; } = null!;
    }
}