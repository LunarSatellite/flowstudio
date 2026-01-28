using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Flow
{
    public class CreateFlowRequest
    {
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public FlowType Type { get; set; } = FlowType.ChatBot;
        public FlowCategory Category { get; set; } = FlowCategory.CustomerService;
        public Guid? TemplateId { get; set; }
    }
}