using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.AI
{
    public class AICompletionResponseDTO
    {
        public string Content { get; set; } = string.Empty;
        public int InputTokens { get; set; }
        public int OutputTokens { get; set; }
        public int TotalTokens { get; set; }
        public decimal Cost { get; set; }
        public int LatencyMs { get; set; }
        public string Model { get; set; } = string.Empty;
    }
}