
namespace Aurora.FlowStudio.Entity.DTO.Base
{
    public class FlowExecutionResponse
    {
        public Guid ConversationId { get; set; }
        public Guid MessageId { get; set; }
        public Guid NodeId { get; set; }
        public string NodeType { get; set; } = string.Empty;

        // Response Content
        public string Content { get; set; } = string.Empty;
        public string ContentType { get; set; } = "text";
        public Dictionary<string, object> RichContent { get; set; } = new();

        // Source Information (if enabled in node config)
        public ResponseSourceInfo? SourceInfo { get; set; }

        // Next Actions
        public List<NextAction> NextActions { get; set; } = new();

        // Client Metadata (custom data from node configuration)
        public Dictionary<string, object> ClientMetadata { get; set; } = new();

        // Conversation State
        public ConversationState State { get; set; } = new();

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}