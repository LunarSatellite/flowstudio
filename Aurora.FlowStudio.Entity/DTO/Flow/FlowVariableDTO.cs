using System;
using Aurora.FlowStudio.Entity.DTO.Base;

namespace Aurora.FlowStudio.Entity.DTO.Flow
{
    public class FlowVariableDTO : TenantBaseDTO
    {
        public Guid FlowId { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }
        public string DefaultValue { get; set; }
        public string Description { get; set; }
        public bool IsRequired { get; set; }
        public string ValidationRule { get; set; }
    }
}
