using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;

namespace Aurora.FlowStudio.Entity.AI
{
    /// <summary>
    /// WebRTC configuration for voice calls
    /// </summary>
    [Table("WebRTCConfigs")]
    public class WebRTCConfig : TenantBaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string StunServer { get; set; }
        
        [MaxLength(255)]
        public string TurnServer { get; set; }
        
        [MaxLength(255)]
        public string TurnUsername { get; set; }
        
        [MaxLength(255)]
        public string TurnPassword { get; set; }
        
        [Column(TypeName = "jsonb")]
        public Dictionary<string, object> AudioSettings { get; set; }
        
        [Column(TypeName = "jsonb")]
        public Dictionary<string, object> VideoSettings { get; set; }
        
        public bool IsDefault { get; set; }
        
        public bool IsActive { get; set; }
    }
}
