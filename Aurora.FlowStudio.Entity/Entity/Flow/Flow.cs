using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ConversationEntity = Aurora.FlowStudio.Entity.Entity.Conversation.Conversation;

namespace Aurora.FlowStudio.Entity.Entity.Flow
{
    /// <summary>
    /// Flow - Conversational flow with comprehensive features for enterprise complexity
    /// </summary>
    [Table("Flows", Schema = "flow")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class Flow : TenantBaseEntity
    {
        // Basic Information
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string DisplayName { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        [MaxLength(4000)]
        public string? LongDescription { get; set; }
        [MaxLength(200)]
        public string? Slug { get; set; }
        
        // Classification
        public FlowType Type { get; set; } = FlowType.ChatBot;
        public FlowCategory Category { get; set; } = FlowCategory.CustomerService;
        public List<string> SubCategories { get; set; } = new();
        public List<string> Tags { get; set; } = new();
        public List<string> Keywords { get; set; } = new();
        
        // Visual Identity
        [MaxLength(2000)]
        public string? IconUrl { get; set; }
        [MaxLength(2000)]
        public string? BannerUrl { get; set; }
        [MaxLength(2000)]
        public string? ThumbnailUrl { get; set; }
        [MaxLength(200)]
        public string? PrimaryColor { get; set; }
        [MaxLength(200)]
        public string? SecondaryColor { get; set; }
        [MaxLength(200)]
        public string? BackgroundColor { get; set; }
        [MaxLength(200)]
        public string? ThemeName { get; set; }
        public Dictionary<string, string> CustomTheme { get; set; } = new();
        
        // Status & Versioning
        public FlowStatus Status { get; set; } = FlowStatus.Draft;
        public int Version { get; set; } = 1;
        public int MajorVersion { get; set; } = 1;
        public int MinorVersion { get; set; } = 0;
        public int PatchVersion { get; set; } = 0;
        [MaxLength(200)]
        public string? VersionLabel { get; set; }
        [MaxLength(200)]
        public string? VersionNotes { get; set; }
        public Guid? ParentFlowId { get; set; }
        public Guid? LatestVersionFlowId { get; set; }
        public bool IsLatestVersion { get; set; } = true;
        [Column(TypeName = "datetime2")]
        public DateTime? DeprecatedAt { get; set; }
        [MaxLength(200)]
        public string? DeprecationReason { get; set; }
        public Guid? ReplacedByFlowId { get; set; }
        
        // Template Management
        public bool IsTemplate { get; set; } = false;
        [MaxLength(200)]
        public string? TemplateCategory { get; set; }
        public int? TemplateDownloads { get; set; }
        public double? TemplateRating { get; set; }
        public int? TemplateRatingCount { get; set; }
        public bool IsPremiumTemplate { get; set; } = false;
        [Column(TypeName = "decimal(18,2)")]
        public decimal? TemplatePrice { get; set; }
        public Guid? ClonedFromFlowId { get; set; }
        public int CloneCount { get; set; } = 0;
        
        // Publishing & Access
        public bool IsPublic { get; set; } = false;
        public bool IsPublished { get; set; } = false;
        [MaxLength(2000)]
        public string? PublishedUrl { get; set; }
        [MaxLength(2000)]
        public string? PublicShareUrl { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? PublishedAt { get; set; }
        [MaxLength(200)]
        public string? PublishedBy { get; set; }
        public Guid? PublishedByUserId { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? LastPublishedAt { get; set; }
        public bool RequireAuthentication { get; set; } = false;
        public bool AllowGuestAccess { get; set; } = true;
        [MaxLength(200)]
        public string? AccessPassword { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? AccessExpiresAt { get; set; }
        public int? MaxAccessCount { get; set; }
        public int CurrentAccessCount { get; set; } = 0;
        
        // Channels & Deployment
        public List<string> EnabledChannels { get; set; } = new(); // WebChat, Voice, WhatsApp, etc.
        public bool EnableWebChat { get; set; } = true;
        public bool EnableVoice { get; set; } = false;
        public bool EnableSMS { get; set; } = false;
        public bool EnableWhatsApp { get; set; } = false;
        public bool EnableTelegram { get; set; } = false;
        public bool EnableFacebook { get; set; } = false;
        public bool EnableInstagram { get; set; } = false;
        public bool EnableSlack { get; set; } = false;
        public bool EnableTeams { get; set; } = false;
        public bool EnableEmail { get; set; } = false;
        public bool EnableMobileApp { get; set; } = true;
        public Dictionary<string, object> ChannelConfigurations { get; set; } = new();
        
        // Flow Structure
        public Guid? EntryNodeId { get; set; }
        public Guid? FallbackNodeId { get; set; }
        public Guid? ErrorHandlerNodeId { get; set; }
        public int NodeCount { get; set; } = 0;
        public int ConnectionCount { get; set; } = 0;
        public int VariableCount { get; set; } = 0;
        public int IntegrationCount { get; set; } = 0;
        public int MaxDepth { get; set; } = 0; // Maximum conversation depth
        public int AvgPathLength { get; set; } = 0;
        public bool HasLoops { get; set; } = false;
        public bool HasConditionals { get; set; } = false;
        
        // Behavior & Settings
        public int DefaultTimeoutSeconds { get; set; } = 30;
        public int MaxRetries { get; set; } = 3;
        public bool EnableFallback { get; set; } = true;
        [MaxLength(4000)]
        public string? FallbackMessage { get; set; }
        public bool EnableErrorHandling { get; set; } = true;
        [MaxLength(4000)]
        public string? ErrorMessage { get; set; }
        public bool EnableLogging { get; set; } = true;
        [MaxLength(200)]
        public string? LogLevel { get; set; } // Debug, Info, Warning, Error
        public bool EnableAnalytics { get; set; } = true;
        public bool TrackUserJourney { get; set; } = true;
        public bool CollectFeedback { get; set; } = true;
        public bool EnableABTesting { get; set; } = false;
        
        // Multi-language Support
        [MaxLength(200)]
        public string? DefaultLanguage { get; set; }
        public List<string> SupportedLanguages { get; set; } = new();
        public bool EnableAutoTranslation { get; set; } = false;
        public bool EnableLanguageDetection { get; set; } = false;
        public Dictionary<string, Guid> LanguageVariants { get; set; } = new(); // lang -> flowId
        
        // AI & NLU Configuration
        public bool EnableAI { get; set; } = true;
        public bool EnableNLU { get; set; } = true;
        public bool EnableIntentRecognition { get; set; } = true;
        public bool EnableEntityExtraction { get; set; } = true;
        public bool EnableSentimentAnalysis { get; set; } = false;
        public double? IntentConfidenceThreshold { get; set; } = 0.7;
        public Guid? DefaultAIProviderId { get; set; }
        public Guid? DefaultTTSProviderId { get; set; }
        public Guid? DefaultSTTProviderId { get; set; }
        [MaxLength(100)]
        public string? AIModelId { get; set; }
        public double? AITemperature { get; set; } = 0.7;
        public int? AIMaxTokens { get; set; } = 1000;
        [MaxLength(200)]
        public string? AISystemPrompt { get; set; }
        public Dictionary<string, object> AIConfiguration { get; set; } = new();
        
        // Voice Settings (for VoiceBot)
        [MaxLength(100)]
        public string? DefaultVoiceId { get; set; }
        public double? VoiceSpeed { get; set; } = 1.0;
        public double? VoicePitch { get; set; } = 1.0;
        [MaxLength(200)]
        public string? VoiceLanguage { get; set; }
        public bool EnableVoiceInterruption { get; set; } = true;
        public bool EnableBackgroundNoiseSuppression { get; set; } = true;
        public int? SilenceDetectionMs { get; set; } = 1500;
        
        // Performance & Optimization
        public bool EnableCaching { get; set; } = true;
        public int? CacheDurationSeconds { get; set; } = 3600;
        public bool EnableResponseCompression { get; set; } = true;
        public bool EnableLoadBalancing { get; set; } = false;
        public int? MaxConcurrentExecutions { get; set; } = 100;
        public int? ExecutionTimeoutSeconds { get; set; } = 300;
        public int? MaxMessageLength { get; set; } = 4000;
        public bool EnableRateLimiting { get; set; } = true;
        public int? RateLimitPerMinute { get; set; } = 60;
        public int? RateLimitPerHour { get; set; } = 1000;
        
        // Security & Compliance
        public bool EnableDataEncryption { get; set; } = true;
        public bool EnablePIIMasking { get; set; } = false;
        public List<string> PIIFields { get; set; } = new();
        public bool LogSensitiveData { get; set; } = false;
        public bool EnableContentFiltering { get; set; } = true;
        public List<string> BlockedWords { get; set; } = new();
        public bool EnableSpamDetection { get; set; } = true;
        public bool EnableModeration { get; set; } = false;
        public bool RequireConsent { get; set; } = false;
        [MaxLength(4000)]
        public string? ConsentMessage { get; set; }
        [MaxLength(2000)]
        public string? PrivacyPolicyUrl { get; set; }
        [MaxLength(2000)]
        public string? TermsOfServiceUrl { get; set; }
        
        // Business Rules & Compliance
        public List<string> ComplianceStandards { get; set; } = new(); // GDPR, HIPAA, SOC2
        public bool EnableGDPR { get; set; } = false;
        public bool EnableHIPAA { get; set; } = false;
        public bool EnableSOC2 { get; set; } = false;
        public int DataRetentionDays { get; set; } = 90;
        public bool AutoDeleteExpiredData { get; set; } = false;
        public bool AllowDataExport { get; set; } = true;
        public bool AllowDataDeletion { get; set; } = true;
        
        // Notifications & Alerts
        public bool EnableNotifications { get; set; } = false;
        public List<string> NotificationEmails { get; set; } = new();
        public bool NotifyOnError { get; set; } = true;
        public bool NotifyOnHighTraffic { get; set; } = false;
        public bool NotifyOnLowSatisfaction { get; set; } = false;
        public int? LowSatisfactionThreshold { get; set; } = 3;
        
        // Integration & Webhooks
        public bool EnableWebhooks { get; set; } = false;
        public List<string> WebhookUrls { get; set; } = new();
        [MaxLength(200)]
        public string? OnStartWebhook { get; set; }
        [MaxLength(200)]
        public string? OnCompleteWebhook { get; set; }
        [MaxLength(200)]
        public string? OnErrorWebhook { get; set; }
        public Dictionary<string, string> CustomWebhooks { get; set; } = new();
        
        // Testing & Debugging
        public bool EnableTestMode { get; set; } = false;
        public bool EnableDebugMode { get; set; } = false;
        public bool LogDetailedErrors { get; set; } = true;
        public bool SimulateTypingDelay { get; set; } = false;
        public int? TypingDelayMs { get; set; } = 500;
        public List<Guid> TestUserIds { get; set; } = new();
        
        // Analytics & Metrics
        [Column(TypeName = "datetime2")]
        public DateTime? FirstExecutionAt { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? LastExecutionAt { get; set; }
        public long TotalExecutions { get; set; } = 0;
        public long SuccessfulExecutions { get; set; } = 0;
        public long FailedExecutions { get; set; } = 0;
        public double? SuccessRate { get; set; }
        public double? AverageExecutionTimeMs { get; set; }
        public double? AverageSatisfactionScore { get; set; }
        public long TotalMessages { get; set; } = 0;
        public long TotalConversations { get; set; } = 0;
        public double? CompletionRate { get; set; }
        public double? AbandonmentRate { get; set; }
        public double? AverageConversationDuration { get; set; }
        public int? AverageMessagesPerConversation { get; set; }
        public Dictionary<string, long> NodeVisitCounts { get; set; } = new();
        public Dictionary<string, double> PathCompletionRates { get; set; } = new();
        
        // User Engagement
        public long UniqueUsers { get; set; } = 0;
        public long ReturningUsers { get; set; } = 0;
        public double? UserRetentionRate { get; set; }
        public double? DailyActiveUsers { get; set; }
        public double? WeeklyActiveUsers { get; set; }
        public double? MonthlyActiveUsers { get; set; }
        
        // Business Metrics
        [Column(TypeName = "decimal(18,2)")]
        public decimal? TotalRevenue { get; set; }
        public int? TotalOrders { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? AverageOrderValue { get; set; }
        public double? ConversionRate { get; set; }
        public int? LeadsGenerated { get; set; }
        public int? TicketsCreated { get; set; }
        public double? CustomerSatisfaction { get; set; }
        public double? NetPromoterScore { get; set; }
        
        // Cost Tracking
        [Column(TypeName = "decimal(18,2)")]
        public decimal? AITokenCost { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? TTSCost { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? STTCost { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? IntegrationCost { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? TotalCost { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? CostPerConversation { get; set; }
        
        // Scheduling
        public bool EnableScheduling { get; set; } = false;
        [Column(TypeName = "datetime2")]
        public DateTime? ScheduledStartAt { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? ScheduledEndAt { get; set; }
        public List<string> ActiveDaysOfWeek { get; set; } = new(); // Monday, Tuesday, etc.
        [MaxLength(200)]
        public string? ActiveStartTime { get; set; } // HH:mm
        [MaxLength(200)]
        public string? ActiveEndTime { get; set; } // HH:mm
        [MaxLength(4000)]
        public string? OfflineMessage { get; set; }
        public bool EnableHolidaySchedule { get; set; } = false;
        public List<DateTime> Holidays { get; set; } = new();
        
        // Collaboration
        public List<Guid> Collaborators { get; set; } = new();
        public Dictionary<Guid, string> CollaboratorRoles { get; set; } = new(); // userId -> role
        public bool AllowComments { get; set; } = true;
        public int CommentCount { get; set; } = 0;
        public bool IsLocked { get; set; } = false;
        public Guid? LockedBy { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? LockedAt { get; set; }
        
        // Approval Workflow
        public bool RequireApproval { get; set; } = false;
        [MaxLength(200)]
        public string? ApprovalStatus { get; set; } // Pending, Approved, Rejected
        public Guid? ApprovedBy { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? ApprovedAt { get; set; }
        [MaxLength(200)]
        public string? ApprovalComments { get; set; }
        public List<Guid> Approvers { get; set; } = new();
        
        // Documentation
        [MaxLength(2000)]
        public string? DocumentationUrl { get; set; }
        [MaxLength(2000)]
        public string? VideoTutorialUrl { get; set; }
        [MaxLength(4000)]
        public string? HelpText { get; set; }
        public Dictionary<string, string> Documentation { get; set; } = new();
        
        // Custom Configuration
        public Dictionary<string, object> Configuration { get; set; } = new();
        public Dictionary<string, object> Metadata { get; set; } = new();
        public Dictionary<string, object> CustomFields { get; set; } = new();
        
        // External References
        [MaxLength(100)]
        public string? ExternalId { get; set; }
        [MaxLength(200)]
        public string? ExternalSource { get; set; }
        public Dictionary<string, string> ExternalIds { get; set; } = new();

        // Navigation properties
        public ICollection<FlowNode> Nodes { get; set; } = new List<FlowNode>();
        public ICollection<FlowVariable> Variables { get; set; } = new List<FlowVariable>();
        public ICollection<FlowVersion> Versions { get; set; } = new List<FlowVersion>();
        public ICollection<FlowIntegration> Integrations { get; set; } = new List<FlowIntegration>();
        public ICollection<ConversationEntity> Conversations { get; set; } = new List<Entity.Conversation.Conversation>();
        public ICollection<Menu> Menus { get; set; } = new List<Menu>();
    }
}