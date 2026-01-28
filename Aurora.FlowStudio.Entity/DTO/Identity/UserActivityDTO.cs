using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Identity
{
    public class UserActivityDTO : BaseDTO
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public ActivityAction Action { get; set; }
        public string Entity { get; set; } = string.Empty;
        public string? EntityName { get; set; }
        public string? Description { get; set; }
        public LogSeverity Severity { get; set; }
        public string IpAddress { get; set; } = string.Empty;
    }
}