using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.Conversation
{
    /// <summary>
    /// Communication channel configuration
    /// </summary>
    [Table("Channels")]
    public class Channel : TenantBaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        [Required]
        public ChannelType Type { get; set; }
        
        public bool IsEnabled { get; set; }
        
        [Column(TypeName = "jsonb")]
        public Dictionary<string, object> Configuration { get; set; }
        
        [MaxLength(500)]
        public string WebhookUrl { get; set; }
        
        [MaxLength(255)]
        public string WebhookSecret { get; set; }
        
        public int MessageCount { get; set; }
        
        public DateTime? LastMessageAt { get; set; }
    }
}
