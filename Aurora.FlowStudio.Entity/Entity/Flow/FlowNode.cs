using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.Flow
{
    /// <summary>
    /// Individual step in flow
    /// </summary>
    [Table("FlowNodes")]
    public class FlowNode : TenantBaseEntity
    {
        [Required]
        public Guid FlowId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string NodeId { get; set; }
        
        [Required]
        public NodeType Type { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        [Column(TypeName = "jsonb")]
        public Dictionary<string, object> Configuration { get; set; }
        
        public int PositionX { get; set; }
        
        public int PositionY { get; set; }
        
        public int ExecutionCount { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal AverageDurationMs { get; set; }
    }
}
