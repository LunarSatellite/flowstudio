using System;
using System.Collections.Generic;
using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Knowledge
{
    public class KnowledgeBaseDTO : TenantBaseDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public List<string> Tags { get; set; }
        public string FileUrl { get; set; }
        public string FileType { get; set; }
        public KnowledgeStatus Status { get; set; }
        public int AccessCount { get; set; }
        public DateTime? LastAccessedAt { get; set; }
    }
}
