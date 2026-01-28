using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("DataSourceConnections", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class DataSourceConnection : TenantBaseEntity
    {
        [MaxLength(200)]
        public string? ConnectionString { get; set; }
        [MaxLength(200)]
        public string? Host { get; set; }
        public int? Port { get; set; }
        [MaxLength(200)]
        public string? Database { get; set; }
        [MaxLength(200)]
        public string? Schema { get; set; }
        public bool UseSSL { get; set; } = true;
        public int TimeoutSeconds { get; set; } = 30;
        public int MaxRetries { get; set; } = 3;
        public Dictionary<string, object> CustomSettings { get; set; } = new();
    }
}