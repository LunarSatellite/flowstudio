using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Flow
{
    public class CreateMenuRequest
    {
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string WelcomeMessage { get; set; } = string.Empty;
        public MenuType Type { get; set; } = MenuType.Main;
        public MenuDisplayStyle DisplayStyle { get; set; } = MenuDisplayStyle.List;
    }
}