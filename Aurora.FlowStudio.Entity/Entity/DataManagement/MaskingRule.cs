using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("MaskingRules", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class MaskingRule : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Field { get; set; } = string.Empty;
        public MaskingType Type { get; set; } = MaskingType.Hash;
        [MaxLength(200)]
        public string? Pattern { get; set; }
        public List<string> ExemptRoles { get; set; } = new();
    }
}