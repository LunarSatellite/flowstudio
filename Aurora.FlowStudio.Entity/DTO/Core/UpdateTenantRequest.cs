using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Core
{
    public class UpdateTenantRequest
    {
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public string? LogoUrl { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public TenantStatus? Status { get; set; }
        public TenantPlan? Plan { get; set; }
    }
}