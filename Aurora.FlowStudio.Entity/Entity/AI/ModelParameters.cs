using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AI
{
    [Table("ModelParameterses", Schema = "ai")]

        public class ModelParameters
    {
        public double Temperature { get; set; } = 0.7;
        public double TopP { get; set; } = 1.0;
        public int MaxTokens { get; set; } = 1000;
        public double FrequencyPenalty { get; set; } = 0.0;
        public double PresencePenalty { get; set; } = 0.0;
        public List<string> StopSequences { get; set; } = new();
        public Dictionary<string, object> CustomParameters { get; set; } = new();
    }
}