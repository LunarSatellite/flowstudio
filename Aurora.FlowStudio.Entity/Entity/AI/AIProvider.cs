using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.AI
{
    /// <summary>
    /// LLM provider configuration
    /// </summary>
    [Table("AIProviders")]
    public class AIProvider : TenantBaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [Required]
        public AIProviderType Provider { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Model { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string ApiKey { get; set; }
        
        [MaxLength(500)]
        public string ApiUrl { get; set; }
        
        [Column(TypeName = "jsonb")]
        public Dictionary<string, object> Settings { get; set; }
        
        public bool IsDefault { get; set; }
        
        public bool IsActive { get; set; }
        
        public int RequestCount { get; set; }
        
        public int TokensUsed { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalCost { get; set; }
    }
}
