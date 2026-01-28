using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Flow
{
    public class CreateMenuItemRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public MenuItemType Type { get; set; }
        public Guid? TargetFlowId { get; set; }
        public Guid? TargetMenuId { get; set; }
        public int Order { get; set; }
    }
}