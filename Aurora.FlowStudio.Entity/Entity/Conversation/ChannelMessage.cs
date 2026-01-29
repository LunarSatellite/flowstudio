using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.Conversation
{
    /// <summary>
    /// Raw channel-specific message data
    /// </summary>
    [Table("ChannelMessages")]
    public class ChannelMessage : TenantBaseEntity
    {
        [Required]
        public Guid MessageId { get; set; }
        
        [Required]
        public Guid ChannelId { get; set; }
        
        [Required]
        public MessageDirection Direction { get; set; }
        
        [MaxLength(255)]
        public string ExternalMessageId { get; set; }
        
        [Required]
        public MessageStatus Status { get; set; }
        
        [Column(TypeName = "jsonb")]
        public Dictionary<string, object> RawData { get; set; }
        
        [MaxLength(500)]
        public string ErrorMessage { get; set; }
        
        public int RetryCount { get; set; }
        
        public DateTime? DeliveredAt { get; set; }
        
        public DateTime? ReadAt { get; set; }
    }
}
