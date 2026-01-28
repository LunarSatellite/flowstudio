using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("DataQualityRuleses", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class DataQualityRules : TenantBaseEntity
    {
        public List<QualityCheck> Checks { get; set; } = new();
        public QualityAction OnFailure { get; set; } = QualityAction.Warn;
        public int MinQualityScore { get; set; } = 80;
    }
}