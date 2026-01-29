using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.Flow
{
    /// <summary>
    /// Visual automation workflow
    /// </summary>
    [Table("Flows")]
    public class Flow : TenantBaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        [Required]
        public FlowCategory Category { get; set; }
        
        [Required]
        public FlowStatus Status { get; set; }
        
        [MaxLength(50)]
        public string TriggerType { get; set; }
        
        [MaxLength(200)]
        public string TriggerValue { get; set; }
        
        public DateTime? PublishedAt { get; set; }
        
        public Guid? PublishedBy { get; set; }
        
        public int ExecutionCount { get; set; }
        
        public int SuccessCount { get; set; }
        
        public int FailureCount { get; set; }
        
        [Column(TypeName = "decimal(5,2)")]
        public decimal SuccessRate { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal AverageDurationSeconds { get; set; }
        
        [Column(TypeName = "jsonb")]
        public List<string> Tags { get; set; }
    }
}
