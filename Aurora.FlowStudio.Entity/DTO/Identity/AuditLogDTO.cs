using System;
using System.Collections.Generic;

namespace Aurora.FlowStudio.DTO.Identity
{
    public class AuditLogDTO
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string Action { get; set; }
        public string EntityType { get; set; }
        public Guid? EntityId { get; set; }
        public string Description { get; set; }
        public Dictionary<string, object> OldValues { get; set; }
        public Dictionary<string, object> NewValues { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
