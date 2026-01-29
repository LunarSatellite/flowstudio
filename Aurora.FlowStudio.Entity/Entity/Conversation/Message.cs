using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.Conversation
{
    /// <summary>
    /// Individual message in conversation
    /// </summary>
    [Table("Messages")]
    public class Message : TenantBaseEntity
    {
        [Required]
        public Guid ConversationId { get; set; }
        
        [Required]
        public MessageRole Role { get; set; }
        
        [Required]
        public string Content { get; set; }
        
        [Required]
        public MessageFormat Format { get; set; }
        
        [MaxLength(500)]
        public string AudioUrl { get; set; }
        
        [MaxLength(500)]
        public string ImageUrl { get; set; }
        
        public int? TokenCount { get; set; }
        
        public int? CharacterCount { get; set; }
        
        [Column(TypeName = "jsonb")]
        public Dictionary<string, object> Metadata { get; set; }
        
        [MaxLength(100)]
        public string DetectedIntent { get; set; }
        
        [Column(TypeName = "jsonb")]
        public Dictionary<string, string> ExtractedEntities { get; set; }
        
        [Column(TypeName = "decimal(5,2)")]
        public decimal? SentimentScore { get; set; }
    }
}
