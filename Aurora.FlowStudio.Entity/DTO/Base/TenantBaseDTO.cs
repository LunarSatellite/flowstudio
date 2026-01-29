using System;

namespace Aurora.FlowStudio.Entity.DTO.Base
{
    /// <summary>
    /// Base DTO for tenant-specific data
    /// </summary>
    public abstract class TenantBaseDTO : BaseDTO
    {
        public Guid TenantId { get; set; }
    }
}
