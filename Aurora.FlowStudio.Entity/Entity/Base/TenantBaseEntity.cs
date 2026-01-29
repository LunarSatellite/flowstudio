using System;
using System.ComponentModel.DataAnnotations;

namespace Aurora.FlowStudio.Entity.Base
{
    /// <summary>
    /// Base entity for tenant-specific data (multi-tenancy support)
    /// Inherits all 25+ audit fields from BaseEntity
    /// Auto-filtered by TenantId in global query filters
    /// </summary>
    public abstract class TenantBaseEntity : BaseEntity
    {
        /// <summary>
        /// Tenant ID for multi-tenancy isolation
        /// All queries automatically filter by this field
        /// </summary>
        [Required]
        public Guid TenantId { get; set; }

        /// <summary>
        /// Ensure tenant ID is set during creation
        /// Called automatically by Repository.SetCreatedAuditFields
        /// </summary>
        public virtual void SetTenant(Guid tenantId)
        {
            if (TenantId == Guid.Empty)
            {
                TenantId = tenantId;
            }
        }

        /// <summary>
        /// Validate tenant context (throws if mismatch)
        /// </summary>
        public virtual void ValidateTenant(Guid expectedTenantId)
        {
            if (TenantId != expectedTenantId)
            {
                throw new InvalidOperationException(
                    $"Tenant mismatch. Expected: {expectedTenantId}, Actual: {TenantId}");
            }
        }
    }
}
