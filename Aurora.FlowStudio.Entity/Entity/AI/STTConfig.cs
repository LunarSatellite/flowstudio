using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AI
{
    [Table("STTConfigs", Schema = "ai")]

        public class STTConfig
    {
        [MaxLength(100)]
        public string? ApiKey { get; set; }
        [MaxLength(2000)]
        public string? ApiUrl { get; set; }
        [MaxLength(200)]
        public string? DefaultLanguage { get; set; }
        public bool EnablePunctuation { get; set; } = true;
        public bool EnableDiarization { get; set; } = false;
        public int MaxSpeakers { get; set; } = 2;
        public AudioFormat InputFormat { get; set; } = AudioFormat.WAV;
        public int SampleRate { get; set; } = 16000;
        public Dictionary<string, object> CustomSettings { get; set; } = new();
    }
}