using Microsoft.AspNetCore.Identity;
using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Identity
{
    [Table("UserActivityLogs", Schema = "identity")]

    [Index(nameof(CreatedAt))]

    public class UserActivityLog : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public ActivityAction Action { get; set; } = ActivityAction.View;
        [MaxLength(200)]
        public string Entity { get; set; } = string.Empty;
        public Guid? EntityId { get; set; }
        [MaxLength(200)]
        public string? EntityName { get; set; }
        [MaxLength(200)]
        public string IpAddress { get; set; } = string.Empty;
        [MaxLength(200)]
        public string UserAgent { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        public Dictionary<string, object> Changes { get; set; } = new();
        public LogSeverity Severity { get; set; } = LogSeverity.Info;
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public virtual ApplicationUser User { get; set; } = null!;
        public virtual Core.Tenant Tenant { get; set; } = null!;
    }
}