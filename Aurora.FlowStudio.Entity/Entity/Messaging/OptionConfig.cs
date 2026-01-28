using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Messaging
{
    [Table("OptionConfigs", Schema = "msg")]

    [Index(nameof(CreatedAt))]

    public class OptionConfig : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Label { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Value { get; set; } = string.Empty;
        public bool Selected { get; set; } = false;
    }
}