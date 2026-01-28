using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Flow
{
    [Table("LLMProcessingConfigs", Schema = "flow")]

        public class LLMProcessingConfig
    {
        [MaxLength(200)]
        public string? SystemPrompt { get; set; }
        public double Temperature { get; set; } = 0.7;
        public int MaxTokens { get; set; } = 500;
        public bool StreamResponse { get; set; } = false;
        public List<string> StopSequences { get; set; } = new();
        public bool IncludeConversationHistory { get; set; } = true;
        public int HistoryMessageCount { get; set; } = 5;
        public Dictionary<string, object> CustomParameters { get; set; } = new();
    }
}