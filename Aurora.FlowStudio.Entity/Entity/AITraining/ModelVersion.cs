using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("ModelVersions", Schema = "train")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class ModelVersion : TenantBaseEntity
    {
        public Guid ModelRegistryId { get; set; }
        [MaxLength(200)]
        public string Version { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? ChangeLog { get; set; }
        [MaxLength(2000)]
        public string ModelPath { get; set; } = string.Empty;
        public ModelPerformanceMetrics Performance { get; set; } = new();
        public bool IsActive { get; set; } = false;

        // Navigation properties
        public ModelRegistry ModelRegistry { get; set; } = null!;
    }
}