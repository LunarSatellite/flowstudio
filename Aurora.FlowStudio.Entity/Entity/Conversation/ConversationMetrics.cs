using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Conversation
{
    [Table("ConversationMetricses", Schema = "conv")]

        public class ConversationMetrics
    {
        public TimeSpan? Duration { get; set; }
        public TimeSpan? ResponseTime { get; set; }
        public int UserMessageCount { get; set; }
        public int BotMessageCount { get; set; }
        public int AgentMessageCount { get; set; }
        public bool GoalAchieved { get; set; }
        [MaxLength(200)]
        public string? GoalType { get; set; }
        public double? ConversionValue { get; set; }
        public Dictionary<string, object> CustomMetrics { get; set; } = new();
    }
}