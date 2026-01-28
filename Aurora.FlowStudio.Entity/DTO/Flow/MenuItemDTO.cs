using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Flow
{
    public class MenuItemDTO : BaseDTO
    {
        public Guid MenuId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? IconUrl { get; set; }
        public string? ImageUrl { get; set; }
        public int Order { get; set; }
        public MenuItemType Type { get; set; }
        public Guid? TargetFlowId { get; set; }
        public string? TargetFlowName { get; set; }
        public Guid? TargetMenuId { get; set; }
        public string? DisplayText { get; set; }
        public bool IsVisible { get; set; }
        public bool IsEnabled { get; set; }
    }
}