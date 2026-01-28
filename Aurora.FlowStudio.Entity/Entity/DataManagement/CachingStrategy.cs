using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("CachingStrategies", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class CachingStrategy : TenantBaseEntity
    {
        public bool Enabled { get; set; } = true;
        public CacheType Type { get; set; } = CacheType.Memory;
        public int TTLSeconds { get; set; } = 3600;
        public int MaxCacheSize { get; set; } = 1000;
        public CacheEvictionPolicy EvictionPolicy { get; set; } = CacheEvictionPolicy.LRU;
        public List<string> CacheKeys { get; set; } = new();
        public bool CacheInvalidationEnabled { get; set; } = true;
    }
}