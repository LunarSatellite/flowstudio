using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("AugmentationConfigs", Schema = "train")]

    [Index(nameof(CreatedAt))]

    public class AugmentationConfig : TenantBaseEntity
    {
        public bool SynonymReplacement { get; set; } = true;
        public bool RandomInsertion { get; set; } = false;
        public bool RandomSwap { get; set; } = false;
        public bool RandomDeletion { get; set; } = false;
        public bool BackTranslation { get; set; } = false;
        public bool Paraphrasing { get; set; } = false;
        public double AugmentationRatio { get; set; } = 0.1; // 10% augmentation
        public int MaxAugmentedSamplesPerOriginal { get; set; } = 3;
        public Dictionary<string, object> CustomSettings { get; set; } = new();
    }
}