using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Flow
{
    public class FlowVariableDTO : BaseDTO
    {
        public Guid FlowId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public VariableType Type { get; set; }
        public string? DefaultValue { get; set; }
        public VariableScope Scope { get; set; }
        public bool IsRequired { get; set; }
    }
}