using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Messaging
{
    [Table("PersonalizationRules", Schema = "msg")]

    [Index(nameof(CreatedAt))]

    public class PersonalizationRule : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Condition { get; set; } = string.Empty; // Expression
        [MaxLength(4000)]
        public string PersonalizedContent { get; set; } = string.Empty;
        public int Priority { get; set; } = 0;
    }
}