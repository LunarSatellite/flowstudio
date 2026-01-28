using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Integration
{
    [Table("ConnectionConfigs", Schema = "integration")]

        public class ConnectionConfig
    {
        // API Configuration
        [MaxLength(2000)]
        public string? BaseUrl { get; set; }
        public Dictionary<string, string> DefaultHeaders { get; set; } = new();
        public Dictionary<string, string> DefaultQueryParams { get; set; } = new();

        // Database Configuration
        [MaxLength(200)]
        public string? ConnectionString { get; set; }
        [MaxLength(200)]
        public string? Host { get; set; }
        public int? Port { get; set; }
        [MaxLength(200)]
        public string? Database { get; set; }
        [MaxLength(200)]
        public string? Schema { get; set; }
        public bool UseConnectionPooling { get; set; } = true;
        public int? MaxPoolSize { get; set; }
        public int? MinPoolSize { get; set; }

        // SSL/TLS
        public bool UseSsl { get; set; } = true;
        [MaxLength(2000)]
        public string? SslCertificatePath { get; set; }

        // Proxy
        [MaxLength(2000)]
        public string? ProxyUrl { get; set; }
        public bool UseProxy { get; set; } = false;

        // Custom Properties
        public Dictionary<string, object> CustomProperties { get; set; } = new();
    }
}