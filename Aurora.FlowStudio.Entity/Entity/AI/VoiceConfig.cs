using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AI
{
    [Table("VoiceConfigs", Schema = "ai")]

        public class VoiceConfig
    {
        [MaxLength(100)]
        public string VoiceId { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        [MaxLength(200)]
        public string Language { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Gender { get; set; } = string.Empty;
        public List<string> Accents { get; set; } = new();
        [MaxLength(2000)]
        public string? SampleAudioUrl { get; set; }
        public Dictionary<string, object> Properties { get; set; } = new();
    }
}