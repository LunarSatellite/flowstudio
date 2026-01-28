using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("LearningRateSchedulers", Schema = "train")]

    [Index(nameof(CreatedAt))]

    public class LearningRateScheduler : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Type { get; set; } = "step"; // step, exponential, cosine, plateau
        public double Gamma { get; set; } = 0.1;
        public int StepSize { get; set; } = 10;
        public Dictionary<string, object> CustomSettings { get; set; } = new();
    }
}