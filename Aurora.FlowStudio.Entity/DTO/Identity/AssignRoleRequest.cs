using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Identity
{
    public class AssignRoleRequest
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}