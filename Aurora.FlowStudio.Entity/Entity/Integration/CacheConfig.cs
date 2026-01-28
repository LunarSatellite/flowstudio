using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Integration
{
    [Table("CacheConfigs", Schema = "integration")]

        public class CacheConfig
    {
        public bool EnableCache { get; set; } = false;
        public int CacheDurationSeconds { get; set; } = 300;
        public CacheStrategy Strategy { get; set; } = CacheStrategy.TimeToLive;
        public List<string> CacheKeys { get; set; } = new();
    }
}