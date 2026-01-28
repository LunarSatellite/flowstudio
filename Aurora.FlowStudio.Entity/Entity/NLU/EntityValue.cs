using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.NLU
{
    [Table("EntityValues", Schema = "nlu")]

    [Index(nameof(CreatedAt))]

    public class EntityValue : TenantBaseEntity
    {
        public Guid EntityId { get; set; }
        [MaxLength(200)]
        public string EntityName { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Value { get; set; } = string.Empty;
        public double Confidence { get; set; }
        [MaxLength(200)]
        public string? Alias { get; set; }
    }
}