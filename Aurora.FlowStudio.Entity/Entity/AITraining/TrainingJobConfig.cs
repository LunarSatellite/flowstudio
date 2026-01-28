using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("TrainingJobConfigs", Schema = "train")]

    [Index(nameof(CreatedAt))]

    public class TrainingJobConfig : TenantBaseEntity
    {
        [MaxLength(200)]
        public string ModelArchitecture { get; set; } = "transformer";
        [MaxLength(200)]
        public string? PretrainedModel { get; set; }
        public int Epochs { get; set; } = 10;
        public int BatchSize { get; set; } = 32;
        public double LearningRate { get; set; } = 0.001;
        [MaxLength(200)]
        public string Optimizer { get; set; } = "adam";
        [MaxLength(200)]
        public string LossFunction { get; set; } = "cross_entropy";
        public List<string> Metrics { get; set; } = new() { "accuracy", "f1" };
        public EarlyStoppingConfig? EarlyStopping { get; set; }
        public LearningRateScheduler? LRScheduler { get; set; }
        public bool UseGPU { get; set; } = true;
        public int? GPUCount { get; set; }
        public bool MixedPrecision { get; set; } = false;
        public bool DataParallel { get; set; } = false;
        public int CheckpointFrequency { get; set; } = 1; // Save every N epochs
        public Dictionary<string, object> CustomHyperparameters { get; set; } = new();
    }
}