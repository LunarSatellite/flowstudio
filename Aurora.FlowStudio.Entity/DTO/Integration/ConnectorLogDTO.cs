using System;
using System.Collections.Generic;
using Aurora.FlowStudio.Entity.DTO.Base;

namespace Aurora.FlowStudio.Entity.DTO.Integration
{
    public class ConnectorLogDTO : TenantBaseDTO
    {
        public Guid ConnectorId { get; set; }
        public Guid? EndpointId { get; set; }
        public Guid? ConversationId { get; set; }
        public Guid? FlowExecutionId { get; set; }
        public string Method { get; set; }
        public string Url { get; set; }
        public Dictionary<string, object> Request { get; set; }
        public Dictionary<string, object> Response { get; set; }
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public int DurationMs { get; set; }
        public string ErrorMessage { get; set; }
    }
}
