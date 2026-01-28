using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Flow
{
    public class UpdateFlowRequest
    {
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public string? IconUrl { get; set; }
        public FlowStatus? Status { get; set; }
        public Dictionary<string, object>? Configuration { get; set; }
    }
}