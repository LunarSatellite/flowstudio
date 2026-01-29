using System;
using System.Collections.Generic;
using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Integration
{
    public class ConnectorDTO : TenantBaseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ConnectorType Type { get; set; }
        public string BaseUrl { get; set; }
        public Dictionary<string, object> Authentication { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public int TimeoutSeconds { get; set; }
        public int RetryCount { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastTestedAt { get; set; }
        public bool LastTestSuccess { get; set; }
    }
}
