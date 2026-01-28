using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.AI
{
    public class AICompletionRequest
    {
        public Guid AIProviderId { get; set; }
        public string? ModelId { get; set; }
        public string Prompt { get; set; } = string.Empty;
        public List<ChatMessage> Messages { get; set; } = new();
        public double Temperature { get; set; } = 0.7;
        public int MaxTokens { get; set; } = 1000;
        public bool Stream { get; set; } = false;
        public Dictionary<string, object> Parameters { get; set; } = new();
    }
}