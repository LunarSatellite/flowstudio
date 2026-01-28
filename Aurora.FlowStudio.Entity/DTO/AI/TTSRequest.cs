using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.AI
{
    public class TTSRequest
    {
        public Guid TTSProviderId { get; set; }
        public string Text { get; set; } = string.Empty;
        public string? VoiceId { get; set; }
        public string? Language { get; set; }
        public double Speed { get; set; } = 1.0;
        public double Pitch { get; set; } = 1.0;
        public AudioFormat OutputFormat { get; set; } = AudioFormat.MP3;
    }
}