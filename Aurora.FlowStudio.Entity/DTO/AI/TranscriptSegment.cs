using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.AI
{
    public class TranscriptSegment
    {
        public string Text { get; set; } = string.Empty;
        public double StartTime { get; set; }
        public double EndTime { get; set; }
        public double Confidence { get; set; }
        public int? SpeakerId { get; set; }
    }
}