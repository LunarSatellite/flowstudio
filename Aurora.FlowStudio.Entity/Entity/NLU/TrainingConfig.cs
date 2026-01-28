using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.NLU
{
    [Table("TrainingConfigs", Schema = "nlu")]

    [Index(nameof(CreatedAt))]

    public class TrainingConfig : TenantBaseEntity
    {
        public TrainingAlgorithm Algorithm { get; set; } = TrainingAlgorithm.Transformer;
        public int Epochs { get; set; } = 10;
        public double LearningRate { get; set; } = 0.001;
        public int BatchSize { get; set; } = 32;
        public bool UseTransferLearning { get; set; } = true;
        [MaxLength(200)]
        public string? PretrainedModel { get; set; }
        public Dictionary<string, object> CustomSettings { get; set; } = new();
    }
}