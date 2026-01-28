using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.AI
{
    public class TTSResponseDTO
    {
        public string AudioUrl { get; set; } = string.Empty;
        public byte[]? AudioData { get; set; }
        public AudioFormat Format { get; set; }
        public int DurationMs { get; set; }
        public long FileSizeBytes { get; set; }
    }
}