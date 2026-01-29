using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;

namespace Aurora.FlowStudio.Entity.Knowledge
{
    /// <summary>
    /// Intent classification for NLU
    /// </summary>
    [Table("Intents")]
    public class Intent : TenantBaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        [Column(TypeName = "jsonb")]
        public List<string> Examples { get; set; }
        
        public Guid? FlowId { get; set; }
        
        public int MatchCount { get; set; }
        
        [Column(TypeName = "decimal(5,2)")]
        public decimal Confidence { get; set; }
        
        public bool IsActive { get; set; }
    }
}
