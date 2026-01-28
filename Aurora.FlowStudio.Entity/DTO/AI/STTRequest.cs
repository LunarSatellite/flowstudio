using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.AI
{
    public class STTRequest
    {
        public Guid STTProviderId { get; set; }
        public byte[] AudioData { get; set; } = Array.Empty<byte>();
        public string? AudioUrl { get; set; }
        public string? Language { get; set; }
        public AudioFormat InputFormat { get; set; } = AudioFormat.WAV;
        public bool EnablePunctuation { get; set; } = true;
        public bool EnableDiarization { get; set; } = false;
    }
}