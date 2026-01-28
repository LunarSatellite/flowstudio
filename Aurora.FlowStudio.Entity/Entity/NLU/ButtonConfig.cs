using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.NLU
{
    [Table("ButtonConfigs", Schema = "nlu")]

    [Index(nameof(CreatedAt))]

    public class ButtonConfig : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Type { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Value { get; set; }
        [MaxLength(2000)]
        public string? Url { get; set; }
    }
}