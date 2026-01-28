using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Core
{
    public class TenantDTO : BaseDTO
    {
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Domain { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
        public string ContactEmail { get; set; } = string.Empty;
        public string? ContactPhone { get; set; }
        public TenantStatus Status { get; set; }
        public TenantPlan Plan { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new();
    }
}