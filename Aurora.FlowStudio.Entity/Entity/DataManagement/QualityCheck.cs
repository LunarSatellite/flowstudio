using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("QualityChecks", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class QualityCheck : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        public QualityCheckType Type { get; set; } = QualityCheckType.NotNull;
        [MaxLength(200)]
        public string Field { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Expression { get; set; }
        [MaxLength(4000)]
        public string? ErrorMessage { get; set; }
        public CheckSeverity Severity { get; set; } = CheckSeverity.Error;
    }
}