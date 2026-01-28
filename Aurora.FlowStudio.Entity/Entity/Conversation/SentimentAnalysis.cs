using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Conversation
{
    [Table("SentimentAnalysises", Schema = "conv")]

        public class SentimentAnalysis
    {
        public SentimentType Type { get; set; } = SentimentType.Neutral;
        public double Score { get; set; } // -1 to 1
        public double Confidence { get; set; } // 0 to 1
        public Dictionary<string, double> Emotions { get; set; } = new(); // joy, anger, sadness, etc.
    }
}