using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.AI
{
    public class CreateAIProviderRequest
    {
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public AIProviderType Type { get; set; }
        public Dictionary<string, object> Configuration { get; set; } = new();
    }
}