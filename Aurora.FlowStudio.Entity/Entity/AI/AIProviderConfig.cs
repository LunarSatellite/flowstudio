using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AI
{
    [Table("AIProviderConfigs", Schema = "ai")]

        public class AIProviderConfig
    {
        [MaxLength(100)]
        public string? ApiKey { get; set; }
        [MaxLength(2000)]
        public string? ApiUrl { get; set; }
        [MaxLength(100)]
        public string? OrganizationId { get; set; }
        public int TimeoutSeconds { get; set; } = 60;
        public int MaxRetries { get; set; } = 3;
        public Dictionary<string, string> DefaultHeaders { get; set; } = new();
        public Dictionary<string, object> CustomSettings { get; set; } = new();
    }
}