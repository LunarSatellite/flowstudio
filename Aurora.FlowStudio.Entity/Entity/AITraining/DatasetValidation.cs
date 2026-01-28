using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("DatasetValidations", Schema = "train")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class DatasetValidation : TenantBaseEntity
    {
        public Guid DatasetId { get; set; }
        public ValidationType Type { get; set; } = ValidationType.Automatic;
        public ValidationStatus Status { get; set; } = ValidationStatus.Passed;
        public List<ValidationIssue> Issues { get; set; } = new();
        public int TotalIssues { get; set; } = 0;
        public int CriticalIssues { get; set; } = 0;
        public int WarningIssues { get; set; } = 0;
        [MaxLength(200)]
        public string? RecommendedActions { get; set; }

        // Navigation properties
        public TrainingDataset Dataset { get; set; } = null!;
    }
}