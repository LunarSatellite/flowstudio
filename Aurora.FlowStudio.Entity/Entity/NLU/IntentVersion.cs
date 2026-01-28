using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.NLU
{
    [Table("IntentVersions", Schema = "nlu")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class IntentVersion : TenantBaseEntity
    {
        public Guid IntentId { get; set; }
        public int Version { get; set; }
        [MaxLength(200)]
        public string? ChangeLog { get; set; }
        [MaxLength(200)]
        public string IntentSnapshot { get; set; } = string.Empty; // JSON
        public bool IsActive { get; set; } = false;
        [Column(TypeName = "datetime2")]
        public DateTime TrainedAt { get; set; }
        public IntentMetrics Metrics { get; set; } = new();

        // Navigation properties
        public Intent Intent { get; set; } = null!;
    }
}