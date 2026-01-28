using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Integration
{
    [Table("RetryConfigs", Schema = "integration")]

        public class RetryConfig
    {
        public bool EnableRetry { get; set; } = true;
        public int MaxRetries { get; set; } = 3;
        public int InitialDelayMs { get; set; } = 1000;
        public int MaxDelayMs { get; set; } = 30000;
        public RetryStrategy Strategy { get; set; } = RetryStrategy.ExponentialBackoff;
        public List<int> RetryableStatusCodes { get; set; } = new() { 408, 429, 500, 502, 503, 504 };
    }
}