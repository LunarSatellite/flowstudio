using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.Conversation
{
    /// <summary>
    /// Customer interaction session across any channel
    /// </summary>
    [Table("Conversations")]
    public class Conversation : TenantBaseEntity
    {
        public Guid? CustomerId { get; set; }
        
        [Required]
        public ChannelType Channel { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string SessionId { get; set; }
        
        [Required]
        public ConversationStatus Status { get; set; }
        
        public Guid? ActiveFlowId { get; set; }
        
        [Column(TypeName = "jsonb")]
        public Dictionary<string, object> CurrentState { get; set; }
        
        [Required]
        public DateTime StartedAt { get; set; }
        
        public DateTime? EndedAt { get; set; }
        
        public int MessageCount { get; set; }
        
        public int TokensUsed { get; set; }
        
        public int VoiceMinutesUsed { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalCost { get; set; }
        
        [MaxLength(10)]
        public string Language { get; set; }
        
        public bool IsResolved { get; set; }
        
        public int? Rating { get; set; }
        
        [MaxLength(1000)]
        public string Feedback { get; set; }
    }
}
