using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;

namespace Aurora.FlowStudio.Entity.Knowledge
{
    /// <summary>
    /// Pre-built message template
    /// </summary>
    [Table("MessageTemplates")]
    public class MessageTemplate : TenantBaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Category { get; set; }
        
        [Required]
        public string Content { get; set; }
        
        [Column(TypeName = "jsonb")]
        public List<string> Variables { get; set; }
        
        [Required]
        [MaxLength(10)]
        public string Language { get; set; }
        
        [Column(TypeName = "jsonb")]
        public List<string> Tags { get; set; }
        
        public int UsageCount { get; set; }
    }
}
