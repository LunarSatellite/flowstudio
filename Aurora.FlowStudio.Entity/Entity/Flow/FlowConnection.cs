using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;

namespace Aurora.FlowStudio.Entity.Flow
{
    /// <summary>
    /// Connection between flow nodes
    /// </summary>
    [Table("FlowConnections")]
    public class FlowConnection : TenantBaseEntity
    {
        [Required]
        public Guid FlowId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string SourceNodeId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string TargetNodeId { get; set; }
        
        [MaxLength(100)]
        public string Label { get; set; }
        
        [MaxLength(500)]
        public string Condition { get; set; }
        
        public int ExecutionCount { get; set; }
    }
}
