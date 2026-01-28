using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.AI
{
    public class STTResponseDTO
    {
        public string Text { get; set; } = string.Empty;
        public double Confidence { get; set; }
        public string? DetectedLanguage { get; set; }
        public int DurationMs { get; set; }
        public List<TranscriptSegment> Segments { get; set; } = new();
    }
}