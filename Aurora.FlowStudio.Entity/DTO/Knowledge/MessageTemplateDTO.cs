using System;
using System.Collections.Generic;
using Aurora.FlowStudio.Entity.DTO.Base;

namespace Aurora.FlowStudio.Entity.DTO.Knowledge
{
    public class MessageTemplateDTO : TenantBaseDTO
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Content { get; set; }
        public List<string> Variables { get; set; }
        public string Language { get; set; }
        public List<string> Tags { get; set; }
        public int UsageCount { get; set; }
    }
}
