using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Core
{
    [Table("TenantSettings", Schema = "core")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class TenantSetting : TenantBaseEntity
    {
        [MaxLength(100)]
        public string Key { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Value { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        public SettingType Type { get; set; } = SettingType.String;
        public bool IsEncrypted { get; set; } = false;
        public bool IsPublic { get; set; } = false;
    }
}