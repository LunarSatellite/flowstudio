using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;

namespace Aurora.FlowStudio.Entity.Flow
{
    /// <summary>
    /// Flow version history
    /// </summary>
    [Table("FlowVersions")]
    public class FlowVersion : TenantBaseEntity
    {
        [Required]
        public Guid FlowId { get; set; }
        
        [Required]
        public int Version { get; set; }
        
        [Column(TypeName = "jsonb")]
        public Dictionary<string, object> FlowData { get; set; }
        
        [MaxLength(1000)]
        public string ChangeNotes { get; set; }
        
        [Required]
        public Guid CreatedBy { get; set; }
        
        public bool IsActive { get; set; }
    }
}
