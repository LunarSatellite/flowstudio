using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Core
{
    public class CreateTenantRequest
    {
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Domain { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string? ContactPhone { get; set; }
        public TenantPlan Plan { get; set; } = TenantPlan.Trial;
    }
}