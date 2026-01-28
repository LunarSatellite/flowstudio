using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AI
{
    [Table("TTSConfigs", Schema = "ai")]

        public class TTSConfig
    {
        [MaxLength(100)]
        public string? ApiKey { get; set; }
        [MaxLength(2000)]
        public string? ApiUrl { get; set; }
        public AudioFormat OutputFormat { get; set; } = AudioFormat.MP3;
        public int SampleRate { get; set; } = 24000;
        public double Speed { get; set; } = 1.0;
        public double Pitch { get; set; } = 1.0;
        public double Volume { get; set; } = 1.0;
        public Dictionary<string, object> CustomSettings { get; set; } = new();
    }
}