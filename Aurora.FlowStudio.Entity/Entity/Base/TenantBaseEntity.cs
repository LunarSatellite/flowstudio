using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Base
{
    /// <summary>
    /// Base entity with tenant support for multi-tenancy
    /// </summary>
    [Index(nameof(TenantId))]
    [Index(nameof(TenantId), nameof(IsDeleted))]
    public abstract class TenantBaseEntity : BaseEntity
    {
        // Tenant Association
        [Required]
        public Guid TenantId { get; set; }
        
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string? TenantName { get; set; }
        
        // Tenant-specific Configuration
        public bool IsTenantWide { get; set; } = true;
        public bool IsSharedAcrossTenants { get; set; } = false;
        public List<Guid> SharedWithTenants { get; set; } = new();
        
        // Tenant Isolation
        public string? TenantSchema { get; set; }
        public string? TenantDatabaseId { get; set; }
        
        // Tenant Billing & Usage
        public bool IsBillable { get; set; } = true;
        public decimal? UsageCost { get; set; }
        public string? BillingCategory { get; set; }
        
        // Tenant Permissions
        public bool InheritTenantPermissions { get; set; } = true;
        public Dictionary<string, object> TenantPermissions { get; set; } = new();
        
        // Tenant Quotas & Limits
        public bool CountsTowardQuota { get; set; } = true;
        public string? QuotaCategory { get; set; }
        public long? SizeInBytes { get; set; }
        
        // Cross-Tenant References
        public Guid? OriginTenantId { get; set; }
        public bool IsCopiedFromAnotherTenant { get; set; } = false;
    }
}
