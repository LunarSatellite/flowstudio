using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Flow
{
    public class PublishFlowRequest
    {
        public string? VersionDescription { get; set; }
        public bool MakePublic { get; set; } = false;
    }
}