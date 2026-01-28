using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.NLU
{
    [Table("ModelEvaluations", Schema = "nlu")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class ModelEvaluation : TenantBaseEntity
    {
        public Guid ModelId { get; set; }
        [MaxLength(200)]
        public string EvaluationName { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        public EvaluationType Type { get; set; } = EvaluationType.TestSet;
        public ModelPerformanceMetrics Results { get; set; } = new();
        [MaxLength(2000)]
        public string? TestDataPath { get; set; }
        public int TestSamples { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public NLUModel Model { get; set; } = null!;
    }
}