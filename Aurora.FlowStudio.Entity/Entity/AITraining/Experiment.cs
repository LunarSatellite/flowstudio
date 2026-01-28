using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("Experiments", Schema = "train")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class Experiment : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        public ExperimentStatus Status { get; set; } = ExperimentStatus.Running;
        public Guid? ParentExperimentId { get; set; }
        public Dictionary<string, object> Parameters { get; set; } = new();
        public Dictionary<string, object> Metrics { get; set; } = new();
        public List<string> Tags { get; set; } = new();
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public ICollection<ExperimentRun> Runs { get; set; } = new List<ExperimentRun>();
    }
}