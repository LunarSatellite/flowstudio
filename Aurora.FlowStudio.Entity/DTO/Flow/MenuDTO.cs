using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Flow
{
    public class MenuDTO : BaseDTO
    {
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public MenuType Type { get; set; }
        public MenuStatus Status { get; set; }
        public string WelcomeMessage { get; set; } = string.Empty;
        public MessageFormat WelcomeMessageFormat { get; set; }
        public string? WelcomeMediaUrl { get; set; }
        public MenuDisplayStyle DisplayStyle { get; set; }
        public List<MenuItemDTO> Items { get; set; } = new();
    }
}