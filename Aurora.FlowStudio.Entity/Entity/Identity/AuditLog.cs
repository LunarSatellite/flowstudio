using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.Identity
{
    /// <summary>
    /// Audit log for tracking user actions
    /// Critical for security, compliance, and debugging
    /// </summary>
    [Table("AuditLogs")]
    public class AuditLog : TenantBaseEntity
    {
        /// <summary>
        /// User who performed the action
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// User email at time of action
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string UserEmail { get; set; }

        /// <summary>
        /// User full name at time of action
        /// </summary>
        [MaxLength(200)]
        public string? UserName { get; set; }

        /// <summary>
        /// Action performed (Create, Update, Delete, View, Login, etc.)
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Action { get; set; }

        /// <summary>
        /// Entity type affected (Conversation, Flow, User, etc.)
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string EntityType { get; set; }

        /// <summary>
        /// Entity ID affected
        /// </summary>
        public Guid? EntityId { get; set; }

        /// <summary>
        /// Description of the action
        /// </summary>
        [MaxLength(1000)]
        public string? Description { get; set; }

        /// <summary>
        /// Old values (before change) - JSON
        /// </summary>
        [Column(TypeName = "jsonb")]
        public Dictionary<string, object>? OldValues { get; set; }

        /// <summary>
        /// New values (after change) - JSON
        /// </summary>
        [Column(TypeName = "jsonb")]
        public Dictionary<string, object>? NewValues { get; set; }

        /// <summary>
        /// IP address from which action was performed
        /// </summary>
        [MaxLength(50)]
        public string? IpAddress { get; set; }

        /// <summary>
        /// User agent (browser, app)
        /// </summary>
        [MaxLength(500)]
        public string? UserAgent { get; set; }

        /// <summary>
        /// Request path (for API calls)
        /// </summary>
        [MaxLength(500)]
        public string? RequestPath { get; set; }

        /// <summary>
        /// HTTP method (GET, POST, PUT, DELETE)
        /// </summary>
        [MaxLength(10)]
        public string? HttpMethod { get; set; }

        /// <summary>
        /// Status code (200, 400, 500, etc.)
        /// </summary>
        public int? StatusCode { get; set; }

        /// <summary>
        /// Duration in milliseconds
        /// </summary>
        public int? DurationMs { get; set; }

        /// <summary>
        /// Was action successful?
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Error message if failed
        /// </summary>
        [MaxLength(2000)]
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Additional metadata
        /// </summary>
        [Column(TypeName = "jsonb")]
        public Dictionary<string, object>? Metadata { get; set; }

        /// <summary>
        /// Session ID
        /// </summary>
        [MaxLength(100)]
        public string? SessionId { get; set; }

        /// <summary>
        /// Correlation ID (for distributed tracing)
        /// </summary>
        [MaxLength(100)]
        public string? CorrelationId { get; set; }

        public AuditLog()
        {
            IsSuccess = true;
            OldValues = new Dictionary<string, object>();
            NewValues = new Dictionary<string, object>();
            Metadata = new Dictionary<string, object>();
        }
    }
}
