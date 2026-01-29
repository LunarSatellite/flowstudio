using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.Knowledge
{
    /// <summary>
    /// Knowledge base document for RAG
    /// </summary>
    [Table("KnowledgeBase")]
    public class KnowledgeBase : TenantBaseEntity
    {
        [Required]
        [MaxLength(500)]
        public string Title { get; set; }
        
        [Required]
        public string Content { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Category { get; set; }
        
        [Column(TypeName = "jsonb")]
        public List<string> Tags { get; set; }
        
        [MaxLength(500)]
        public string FileUrl { get; set; }
        
        [MaxLength(50)]
        public string FileType { get; set; }
        
        [Required]
        public KnowledgeStatus Status { get; set; }
        
        [Column(TypeName = "vector(1536)")]
        public float[] Embeddings { get; set; }
        
        public int AccessCount { get; set; }
        
        public DateTime? LastAccessedAt { get; set; }
    }
}
