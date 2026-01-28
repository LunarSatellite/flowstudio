using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Flow
{
    [Table("FlowVersions", Schema = "flow")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class FlowVersion : TenantBaseEntity
    {
        public Guid FlowId { get; set; }
        public int Version { get; set; }
        [MaxLength(4000)]
        public string? Description { get; set; }
        [MaxLength(200)]
        public string FlowSnapshot { get; set; } = string.Empty; // JSON serialized flow
        [MaxLength(200)]
        public string? ChangeSummary { get; set; }
        public bool IsActive { get; set; } = false;

        // Navigation properties
        public Flow Flow { get; set; } = null!;
    }
}