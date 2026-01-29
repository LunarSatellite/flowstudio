using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.Integration
{
    /// <summary>
    /// Specific API endpoint configuration
    /// </summary>
    [Table("ConnectorEndpoints")]
    public class ConnectorEndpoint : TenantBaseEntity
    {
        [Required]
        public Guid ConnectorId { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        [Required]
        public Enums.HttpMethod Method { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string Path { get; set; }
        
        [Column(TypeName = "jsonb")]
        public Dictionary<string, object> RequestTemplate { get; set; }
        
        [Column(TypeName = "jsonb")]
        public Dictionary<string, object> ResponseMapping { get; set; }
        
        public bool CacheEnabled { get; set; }
        
        public int CacheDurationSeconds { get; set; }
        
        public int ExecutionCount { get; set; }
        
        public int SuccessCount { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal AverageDurationMs { get; set; }
    }
}
