using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.AI
{
    public class TTSProviderDTO : BaseDTO
    {
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public TTSProviderType Type { get; set; }
        public bool IsActive { get; set; }
        public bool IsPlatformDefault { get; set; }
        public List<VoiceConfigDTO> AvailableVoices { get; set; } = new();
        public string? DefaultVoiceId { get; set; }
        public string? DefaultLanguage { get; set; }
    }
}