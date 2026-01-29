using System;
using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Tenant
{
    public class TenantDTO : BaseDTO
    {
        public string CompanyName { get; set; }
        public string Domain { get; set; }
        public TenantStatus Status { get; set; }
        public TenantPlan Plan { get; set; }
        public DateTime? TrialEndsAt { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Timezone { get; set; }
    }
}
