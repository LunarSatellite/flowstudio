using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;

namespace Aurora.FlowStudio.Entity.Integration
{
    /// <summary>
    /// API call logging for debugging
    /// </summary>
    [Table("ConnectorLogs")]
    public class ConnectorLog : TenantBaseEntity
    {
        [Required]
        public Guid ConnectorId { get; set; }
        
        public Guid? EndpointId { get; set; }
        
        public Guid? ConversationId { get; set; }
        
        public Guid? FlowExecutionId { get; set; }
        
        [Required]
        [MaxLength(10)]
        public string Method { get; set; }
        
        [Required]
        [MaxLength(1000)]
        public string Url { get; set; }
        
        [Column(TypeName = "jsonb")]
        public Dictionary<string, object> Request { get; set; }
        
        [Column(TypeName = "jsonb")]
        public Dictionary<string, object> Response { get; set; }
        
        public int StatusCode { get; set; }
        
        public bool IsSuccess { get; set; }
        
        public int DurationMs { get; set; }
        
        [MaxLength(1000)]
        public string ErrorMessage { get; set; }
    }
}
