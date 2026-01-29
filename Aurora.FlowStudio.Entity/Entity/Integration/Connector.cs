using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.Integration
{
    /// <summary>
    /// External API/Database connection
    /// </summary>
    [Table("Connectors")]
    public class Connector : TenantBaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        [Required]
        public ConnectorType Type { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string BaseUrl { get; set; }
        
        [Column(TypeName = "jsonb")]
        public Dictionary<string, object> Authentication { get; set; }
        
        [Column(TypeName = "jsonb")]
        public Dictionary<string, string> Headers { get; set; }
        
        public int TimeoutSeconds { get; set; }
        
        public int RetryCount { get; set; }
        
        public bool IsActive { get; set; }
        
        public DateTime? LastTestedAt { get; set; }
        
        public bool LastTestSuccess { get; set; }
    }
}
