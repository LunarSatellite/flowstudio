using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;

namespace Aurora.FlowStudio.Entity.Flow
{
    /// <summary>
    /// Variable definition for flow
    /// </summary>
    [Table("FlowVariables")]
    public class FlowVariable : TenantBaseEntity
    {
        [Required]
        public Guid FlowId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string DataType { get; set; }
        
        [MaxLength(500)]
        public string DefaultValue { get; set; }
        
        [MaxLength(500)]
        public string Description { get; set; }
        
        public bool IsRequired { get; set; }
        
        [MaxLength(500)]
        public string ValidationRule { get; set; }
    }
}
