using System;
using System.Collections.Generic;

namespace Aurora.FlowStudio.DTO.Identity
{
    public class ApplicationRoleDTO
    {
        public Guid Id { get; set; }
        public Guid? TenantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsSystemRole { get; set; }
        public bool IsActive { get; set; }
        public List<string> Permissions { get; set; }
        public int UserCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
