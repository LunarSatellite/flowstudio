using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;

namespace Aurora.FlowStudio.Entity.Tenant
{
    /// <summary>
    /// API authentication keys for tenant
    /// </summary>
    [Table("APIKeys")]
    public class APIKey : TenantBaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Key { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        [Column(TypeName = "jsonb")]
        public List<string> AllowedIPs { get; set; }
        
        [Column(TypeName = "jsonb")]
        public List<string> AllowedOrigins { get; set; }
        
        public DateTime? ExpiresAt { get; set; }
        
        public DateTime? LastUsedAt { get; set; }
        
        public bool IsActive { get; set; }
        
        public int RequestCount { get; set; }
    }
}
