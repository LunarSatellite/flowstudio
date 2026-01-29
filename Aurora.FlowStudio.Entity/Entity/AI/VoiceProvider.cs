using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.AI
{
    /// <summary>
    /// TTS/STT provider configuration
    /// </summary>
    [Table("VoiceProviders")]
    public class VoiceProvider : TenantBaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [Required]
        public VoiceProviderType Provider { get; set; }
        
        [Required]
        public VoiceType Type { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string ApiKey { get; set; }
        
        [Column(TypeName = "jsonb")]
        public Dictionary<string, object> Settings { get; set; }
        
        public bool IsDefault { get; set; }
        
        public bool IsActive { get; set; }
        
        public int RequestCount { get; set; }
        
        public int MinutesUsed { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalCost { get; set; }
    }
}
