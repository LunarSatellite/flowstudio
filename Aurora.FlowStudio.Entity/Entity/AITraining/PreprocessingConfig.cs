using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("PreprocessingConfigs", Schema = "train")]

    [Index(nameof(CreatedAt))]

    public class PreprocessingConfig : TenantBaseEntity
    {
        public bool Lowercase { get; set; } = true;
        public bool RemovePunctuation { get; set; } = false;
        public bool RemoveStopWords { get; set; } = false;
        public bool Stemming { get; set; } = false;
        public bool Lemmatization { get; set; } = true;
        public bool RemoveAccents { get; set; } = false;
        public bool RemoveNumbers { get; set; } = false;
        public bool RemoveUrls { get; set; } = true;
        public bool RemoveEmails { get; set; } = true;
        public bool RemoveEmojis { get; set; } = false;
        public bool ExpandContractions { get; set; } = true;
        public int? MaxLength { get; set; }
        public int? MinLength { get; set; }
        public List<string> CustomStopWords { get; set; } = new();
        public Dictionary<string, object> CustomSettings { get; set; } = new();
    }
}