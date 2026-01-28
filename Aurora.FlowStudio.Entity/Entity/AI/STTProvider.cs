using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AI
{
    [Table("STTProviders", Schema = "ai")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class STTProvider : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string DisplayName { get; set; } = string.Empty;
        public STTProviderType Type { get; set; } = STTProviderType.PlatformDefault;
        public bool IsActive { get; set; } = true;
        public bool IsPlatformDefault { get; set; } = false;
        public STTConfig Configuration { get; set; } = new();
        public List<string> SupportedLanguages { get; set; } = new();
        public Dictionary<string, object> Metadata { get; set; } = new();
    }
}