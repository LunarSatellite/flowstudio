using System;

namespace Aurora.FlowStudio.Entity.Enums
{
    // TENANT & BILLING
    public enum TenantStatus { Active, Suspended, Cancelled, Trial }
    public enum TenantPlan { Free, Basic, Pro, Enterprise }
    public enum SubscriptionStatus { Active, Cancelled, Expired, PastDue }
    public enum UsageType { Token, Voice, SMS, Email, API }
    public enum InvoiceStatus { Draft, Pending, Paid, Failed, Refunded }
    public enum AlertType { Budget, Usage, Quota, Spike }
    public enum AlertPeriod { Hourly, Daily, Weekly, Monthly }
    public enum QuotaPeriod { Daily, Weekly, Monthly, Yearly }
    public enum QuotaAction { Pause, Notify, Allow }

    // CONVERSATION
    public enum ChannelType { Web, WhatsApp, Voice, SMS, Email }
    public enum ConversationStatus { Active, Completed, Abandoned, Transferred }
    public enum MessageRole { User, Assistant, System }
    public enum MessageFormat { Text, Audio, Image, Video, Document }
    public enum MessageDirection { Inbound, Outbound }
    public enum MessageStatus { Pending, Sent, Delivered, Read, Failed }

    // FLOW
    public enum FlowCategory { Support, Sales, Marketing, Custom }
    public enum FlowStatus { Draft, Published, Archived }
    public enum NodeType { Message, API, Condition, AI, Transfer, Wait, Input, Variable }
    public enum ExecutionStatus { Running, Completed, Failed, Cancelled }

    // AI & INTEGRATION
    public enum AIProviderType { OpenAI, Anthropic, Google, Azure, Groq, Custom }
    public enum VoiceProviderType { ElevenLabs, Google, Azure, Deepgram, AssemblyAI, Custom }
    public enum VoiceType { TTS, STT, Both }
    public enum ConnectorType { REST, GraphQL, Database, Webhook, SOAP }
    public enum HttpMethod { GET, POST, PUT, DELETE, PATCH }

    // KNOWLEDGE & ACCESS
    public enum KnowledgeStatus { Draft, Published, Archived }
    public enum UserRole { Admin, Manager, Agent, Viewer }
}
