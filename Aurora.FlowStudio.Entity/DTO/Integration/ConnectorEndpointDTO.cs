using System;
using System.Collections.Generic;
using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;
using HttpMethod = Aurora.FlowStudio.Entity.Enums.HttpMethod;

namespace Aurora.FlowStudio.Entity.DTO.Integration
{
    public class ConnectorEndpointDTO : TenantBaseDTO
    {
        public Guid ConnectorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public HttpMethod Method { get; set; }
        public string Path { get; set; }
        public Dictionary<string, object> RequestTemplate { get; set; }
        public Dictionary<string, object> ResponseMapping { get; set; }
        public bool CacheEnabled { get; set; }
        public int CacheDurationSeconds { get; set; }
        public int ExecutionCount { get; set; }
        public int SuccessCount { get; set; }
        public decimal AverageDurationMs { get; set; }
    }
}
