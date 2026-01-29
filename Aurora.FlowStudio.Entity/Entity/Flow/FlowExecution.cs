using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.Flow
{
    /// <summary>
    /// Runtime flow execution tracking
    /// </summary>
    [Table("FlowExecutions")]
    public class FlowExecution : TenantBaseEntity
    {
        [Required]
        public Guid FlowId { get; set; }
        
        [Required]
        public Guid ConversationId { get; set; }
        
        [MaxLength(100)]
        public string CurrentNodeId { get; set; }
        
        [Column(TypeName = "jsonb")]
        public Dictionary<string, object> Variables { get; set; }
        
        [Required]
        public ExecutionStatus Status { get; set; }
        
        [Required]
        public DateTime StartedAt { get; set; }
        
        public DateTime? CompletedAt { get; set; }
        
        public int DurationMs { get; set; }
        
        [MaxLength(1000)]
        public string ErrorMessage { get; set; }
        
        [MaxLength(100)]
        public string ErrorNodeId { get; set; }
        
        [Column(TypeName = "jsonb")]
        public List<string> ExecutedNodes { get; set; }
    }
}
