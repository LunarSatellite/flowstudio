using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("EntityLabels", Schema = "train")]

    [Index(nameof(CreatedAt))]

    public class EntityLabel : TenantBaseEntity
    {
        [MaxLength(200)]
        public string EntityType { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Value { get; set; } = string.Empty;
        public int StartPosition { get; set; }
        public int EndPosition { get; set; }
    }
}