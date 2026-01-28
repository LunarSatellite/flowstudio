using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("DataAccessPolicies", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class DataAccessPolicy : TenantBaseEntity
    {
        public bool EnableRowLevelSecurity { get; set; } = false;
        public bool EnableColumnLevelSecurity { get; set; } = false;
        public List<AccessRule> Rules { get; set; } = new();
        public bool EnableAuditLogging { get; set; } = true;
        public bool EnableDataMasking { get; set; } = false;
        public List<MaskingRule> MaskingRules { get; set; } = new();
    }
}