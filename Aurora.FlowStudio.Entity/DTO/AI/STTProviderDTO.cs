using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.AI
{
    public class STTProviderDTO : BaseDTO
    {
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public STTProviderType Type { get; set; }
        public bool IsActive { get; set; }
        public bool IsPlatformDefault { get; set; }
        public List<string> SupportedLanguages { get; set; } = new();
    }
}