using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.AI
{
    public class VoiceConfigDTO
    {
        public string VoiceId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Language { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public List<string> Accents { get; set; } = new();
        public string? SampleAudioUrl { get; set; }
    }
}