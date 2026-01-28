using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.NLU
{
    [Table("ModelTrainingLogs", Schema = "nlu")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class ModelTrainingLog : TenantBaseEntity
    {
        public Guid ModelId { get; set; }
        public int Epoch { get; set; }
        public double TrainingLoss { get; set; }
        public double ValidationLoss { get; set; }
        public double TrainingAccuracy { get; set; }
        public double ValidationAccuracy { get; set; }
        public TimeSpan Duration { get; set; }
        public Dictionary<string, object> Metrics { get; set; } = new();

        // Navigation properties
        public NLUModel Model { get; set; } = null!;
    }
}