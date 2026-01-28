using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AI
{
    [Table("TTSProviders", Schema = "ai")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class TTSProvider : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string DisplayName { get; set; } = string.Empty;
        public TTSProviderType Type { get; set; } = TTSProviderType.PlatformDefault;
        public bool IsActive { get; set; } = true;
        public bool IsPlatformDefault { get; set; } = false;
        public TTSConfig Configuration { get; set; } = new();
        public List<VoiceConfig> AvailableVoices { get; set; } = new();
        [MaxLength(100)]
        public string? DefaultVoiceId { get; set; }
        [MaxLength(200)]
        public string? DefaultLanguage { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new();
    }
}