using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Integration
{
    [Table("RateLimitConfigs", Schema = "integration")]

        public class RateLimitConfig
    {
        public int MaxRequests { get; set; } = 100;
        public int WindowSeconds { get; set; } = 60;
        public RateLimitStrategy Strategy { get; set; } = RateLimitStrategy.SlidingWindow;
        public bool EnableRateLimiting { get; set; } = true;
    }
}