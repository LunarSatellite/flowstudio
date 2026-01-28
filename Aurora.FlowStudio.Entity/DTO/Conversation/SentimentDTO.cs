using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Conversation
{
    public class SentimentDTO
    {
        public SentimentType Type { get; set; }
        public double Score { get; set; }
        public double Confidence { get; set; }
        public Dictionary<string, double> Emotions { get; set; } = new();
    }
}