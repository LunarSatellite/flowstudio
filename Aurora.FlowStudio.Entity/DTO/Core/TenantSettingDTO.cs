using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Core
{
    public class TenantSettingDTO : BaseDTO
    {
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string? Description { get; set; }
        public SettingType Type { get; set; }
        public bool IsPublic { get; set; }
    }
}