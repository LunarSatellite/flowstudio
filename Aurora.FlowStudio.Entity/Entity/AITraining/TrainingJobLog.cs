using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("TrainingJobLogs", Schema = "train")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class TrainingJobLog : TenantBaseEntity
    {
        public Guid TrainingJobId { get; set; }
        public LogLevel Level { get; set; } = LogLevel.Info;
        [MaxLength(4000)]
        public string Message { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Source { get; set; }
        public Dictionary<string, object> Data { get; set; } = new();

        // Navigation properties
        public TrainingJob TrainingJob { get; set; } = null!;
    }
}