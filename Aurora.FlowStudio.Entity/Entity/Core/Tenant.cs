using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using FlowEntity = Aurora.FlowStudio.Entity.Entity.Flow.Flow;

namespace Aurora.FlowStudio.Entity.Entity.Core
{
    /// <summary>
    /// Tenant - Multi-tenant organization with comprehensive enterprise features
    /// </summary>
    [Table("Tenants", Schema = "core")]

    [Index(nameof(CreatedAt))]

    public class Tenant : BaseEntity
    {
        // Basic Information
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string DisplayName { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        [MaxLength(200)]
        public string? Slug { get; set; }
        [MaxLength(200)]
        public string Domain { get; set; } = string.Empty;
        public List<string> AlternateDomains { get; set; } = new();
        [MaxLength(200)]
        public string? SubDomain { get; set; }
        [MaxLength(200)]
        public string? CustomDomain { get; set; }
        
        // Branding
        [MaxLength(2000)]
        public string? LogoUrl { get; set; }
        [MaxLength(2000)]
        public string? FaviconUrl { get; set; }
        [MaxLength(2000)]
        public string? BannerUrl { get; set; }
        [MaxLength(200)]
        public string? PrimaryColor { get; set; }
        [MaxLength(200)]
        public string? SecondaryColor { get; set; }
        [MaxLength(200)]
        public string? AccentColor { get; set; }
        public Dictionary<string, string> BrandingTheme { get; set; } = new();
        [MaxLength(200)]
        public string? CustomCss { get; set; }
        [MaxLength(200)]
        public string? CustomScript { get; set; }
        
        // Contact Information
        [MaxLength(255)]
        public string ContactEmail { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? ContactPhone { get; set; }
        [MaxLength(255)]
        public string? SupportEmail { get; set; }
        [MaxLength(255)]
        public string? SalesEmail { get; set; }
        [MaxLength(255)]
        public string? BillingEmail { get; set; }
        [MaxLength(200)]
        public string? Website { get; set; }
        
        // Address Information
        [MaxLength(200)]
        public string? Address1 { get; set; }
        [MaxLength(200)]
        public string? Address2 { get; set; }
        [MaxLength(200)]
        public string? City { get; set; }
        [MaxLength(200)]
        public string? State { get; set; }
        [MaxLength(200)]
        public string? Country { get; set; }
        [MaxLength(100)]
        public string? PostalCode { get; set; }
        [MaxLength(200)]
        public string? Region { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Latitude { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Longitude { get; set; }
        
        // Localization
        [MaxLength(200)]
        public string? TimeZone { get; set; }
        [MaxLength(200)]
        public string? Currency { get; set; }
        [MaxLength(200)]
        public string? Language { get; set; }
        public List<string> SupportedLanguages { get; set; } = new();
        public List<string> SupportedCurrencies { get; set; } = new();
        [MaxLength(200)]
        public string? DateFormat { get; set; }
        [MaxLength(200)]
        public string? TimeFormat { get; set; }
        [MaxLength(200)]
        public string? NumberFormat { get; set; }
        
        // Status & Plan
        public TenantStatus Status { get; set; } = TenantStatus.Active;
        public TenantPlan Plan { get; set; } = TenantPlan.Trial;
        [MaxLength(200)]
        public string? CustomPlanName { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? ActivatedAt { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? SuspendedAt { get; set; }
        [MaxLength(200)]
        public string? SuspensionReason { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? CancelledAt { get; set; }
        [MaxLength(200)]
        public string? CancellationReason { get; set; }
        
        // Subscription & Billing
        [Column(TypeName = "datetime2")]
        public DateTime? TrialStartsAt { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? TrialEndsAt { get; set; }
        public int TrialDays { get; set; } = 14;
        public bool TrialExtended { get; set; } = false;
        [Column(TypeName = "datetime2")]
        public DateTime? SubscriptionStartsAt { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? SubscriptionEndsAt { get; set; }
        [MaxLength(100)]
        public string? SubscriptionId { get; set; }
        [MaxLength(100)]
        public string? CustomerId { get; set; }
        [MaxLength(100)]
        public string? PaymentMethodId { get; set; }
        [MaxLength(200)]
        public string? BillingCycle { get; set; } // monthly, yearly, quarterly
        [Column(TypeName = "decimal(18,2)")]
        public decimal? MonthlyRevenue { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? AnnualRevenue { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? LifetimeValue { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? NextBillingDate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? LastPaymentDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? LastPaymentAmount { get; set; }
        public bool AutoRenew { get; set; } = true;
        public int GracePeriodDays { get; set; } = 7;
        
        // Resource Limits & Quotas
        public int MaxUsers { get; set; } = 5;
        public int MaxAdmins { get; set; } = 2;
        public int MaxFlows { get; set; } = 10;
        public int MaxActiveFlows { get; set; } = 5;
        public int MaxConversationsPerMonth { get; set; } = 1000;
        public int MaxMessagesPerConversation { get; set; } = 500;
        public int MaxCustomers { get; set; } = 5000;
        public int MaxConnectors { get; set; } = 10;
        public int MaxAPICallsPerMonth { get; set; } = 100000;
        public long MaxStorageBytes { get; set; } = 10737418240; // 10GB
        public long MaxFileUploadSizeBytes { get; set; } = 10485760; // 10MB
        public int MaxConcurrentConversations { get; set; } = 100;
        public int MaxWebhooksPerFlow { get; set; } = 5;
        public int MaxAITokensPerMonth { get; set; } = 1000000;
        public int MaxTTSCharactersPerMonth { get; set; } = 500000;
        public int MaxSTTMinutesPerMonth { get; set; } = 1000;
        
        // Current Usage (for quota tracking)
        public int CurrentUsers { get; set; } = 0;
        public int CurrentFlows { get; set; } = 0;
        public int CurrentConversationsThisMonth { get; set; } = 0;
        public int CurrentAPICallsThisMonth { get; set; } = 0;
        public long CurrentStorageUsedBytes { get; set; } = 0;
        public int CurrentAITokensThisMonth { get; set; } = 0;
        public int CurrentTTSCharactersThisMonth { get; set; } = 0;
        public int CurrentSTTMinutesThisMonth { get; set; } = 0;
        [Column(TypeName = "datetime2")]
        public DateTime? UsageResetAt { get; set; }
        
        // Features & Capabilities
        public bool EnableVoiceBot { get; set; } = false;
        public bool EnableChatBot { get; set; } = true;
        public bool EnableMultiChannel { get; set; } = false;
        public bool EnableAnalytics { get; set; } = true;
        public bool EnableCustomBranding { get; set; } = false;
        public bool EnableCustomDomain { get; set; } = false;
        public bool EnableSSO { get; set; } = false;
        public bool EnableFIDO2 { get; set; } = true;
        public bool EnableAPIAccess { get; set; } = true;
        public bool EnableWebhooks { get; set; } = true;
        public bool EnableAdvancedAnalytics { get; set; } = false;
        public bool EnableWhiteLabeling { get; set; } = false;
        public bool EnablePrioritySupport { get; set; } = false;
        public bool EnableDedicatedAccount { get; set; } = false;
        public bool EnableCustomIntegrations { get; set; } = false;
        public bool EnableAIFeatures { get; set; } = true;
        public bool EnableAdvancedSecurity { get; set; } = false;
        public bool EnableDataExport { get; set; } = true;
        public bool EnableAuditLogs { get; set; } = true;
        public bool EnableIPWhitelist { get; set; } = false;
        public bool EnableRoleBasedAccess { get; set; } = true;
        public List<string> EnabledFeatures { get; set; } = new();
        
        // Security & Compliance
        public List<string> AllowedIPAddresses { get; set; } = new();
        public bool RequireMFA { get; set; } = false;
        public bool RequireFIDO2 { get; set; } = true;
        public int PasswordMinLength { get; set; } = 12;
        public bool PasswordRequireUppercase { get; set; } = true;
        public bool PasswordRequireLowercase { get; set; } = true;
        public bool PasswordRequireNumber { get; set; } = true;
        public bool PasswordRequireSpecialChar { get; set; } = true;
        public int PasswordExpiryDays { get; set; } = 90;
        public int MaxLoginAttempts { get; set; } = 5;
        public int LockoutDurationMinutes { get; set; } = 30;
        public int SessionTimeoutMinutes { get; set; } = 60;
        public bool EnableSessionConcurrencyLimit { get; set; } = false;
        public int MaxConcurrentSessions { get; set; } = 3;
        public bool LogSecurityEvents { get; set; } = true;
        public bool EnableDataEncryption { get; set; } = true;
        [MaxLength(100)]
        public string? EncryptionKeyId { get; set; }
        public List<string> ComplianceStandards { get; set; } = new(); // GDPR, HIPAA, SOC2, etc.
        [Column(TypeName = "datetime2")]
        public DateTime? LastSecurityAuditAt { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? LastComplianceCheckAt { get; set; }
        
        // Data Retention & Privacy
        public int DataRetentionDays { get; set; } = 365;
        public int ConversationRetentionDays { get; set; } = 90;
        public int LogRetentionDays { get; set; } = 30;
        public bool AutoDeleteOldData { get; set; } = false;
        public bool EnableGDPRCompliance { get; set; } = true;
        public bool EnableCCPACompliance { get; set; } = false;
        public bool AllowDataExport { get; set; } = true;
        public bool AllowDataDeletion { get; set; } = true;
        [MaxLength(2000)]
        public string? DataProcessingAgreementUrl { get; set; }
        [MaxLength(2000)]
        public string? PrivacyPolicyUrl { get; set; }
        [MaxLength(2000)]
        public string? TermsOfServiceUrl { get; set; }
        
        // Notifications & Alerts
        public bool EnableEmailNotifications { get; set; } = true;
        public bool EnableSMSNotifications { get; set; } = false;
        public bool EnableSlackNotifications { get; set; } = false;
        public bool EnableWebhookNotifications { get; set; } = false;
        [MaxLength(2000)]
        public string? SlackWebhookUrl { get; set; }
        public List<string> NotificationEmails { get; set; } = new();
        public bool NotifyOnQuotaWarning { get; set; } = true;
        public int QuotaWarningThresholdPercent { get; set; } = 80;
        public bool NotifyOnSecurityEvents { get; set; } = true;
        public bool NotifyOnBillingIssues { get; set; } = true;
        public bool NotifyOnSystemMaintenance { get; set; } = true;
        
        // Integration & API
        [MaxLength(100)]
        public string? ApiKey { get; set; }
        [MaxLength(200)]
        public string? ApiSecret { get; set; }
        [MaxLength(200)]
        public string? WebhookSecret { get; set; }
        public List<string> WebhookUrls { get; set; } = new();
        public int RateLimitPerMinute { get; set; } = 60;
        public int RateLimitPerHour { get; set; } = 1000;
        public int RateLimitPerDay { get; set; } = 10000;
        public bool EnableCORS { get; set; } = true;
        public List<string> AllowedOrigins { get; set; } = new();
        
        // Business Information
        [MaxLength(200)]
        public string? CompanyRegistrationNumber { get; set; }
        [MaxLength(100)]
        public string? TaxId { get; set; }
        [MaxLength(200)]
        public string? VATNumber { get; set; }
        [MaxLength(200)]
        public string? Industry { get; set; }
        [MaxLength(200)]
        public string? CompanySize { get; set; } // Small, Medium, Large, Enterprise
        public int? EmployeeCount { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? FoundedDate { get; set; }
        
        // Tracking & Analytics
        [MaxLength(200)]
        public string? UtmSource { get; set; }
        [MaxLength(200)]
        public string? UtmMedium { get; set; }
        [MaxLength(200)]
        public string? UtmCampaign { get; set; }
        [MaxLength(200)]
        public string? ReferralSource { get; set; }
        [MaxLength(200)]
        public string? SignupChannel { get; set; }
        public Guid? ReferredByTenantId { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? FirstPurchaseDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? TotalRevenue { get; set; }
        public int? TotalOrders { get; set; }
        [MaxLength(200)]
        public string? CustomerSuccessManager { get; set; }
        [MaxLength(200)]
        public string? AccountManager { get; set; }
        
        // Internal Notes & Tags
        [MaxLength(200)]
        public string? InternalNotes { get; set; }
        public List<string> Tags { get; set; } = new();
        public int? HealthScore { get; set; } // 0-100
        [MaxLength(200)]
        public string? ChurnRisk { get; set; } // Low, Medium, High
        [Column(TypeName = "datetime2")]
        public DateTime? LastEngagementAt { get; set; }
        public int? NPS { get; set; } // Net Promoter Score
        
        // Custom Fields & Metadata
        public Dictionary<string, object> CustomFields { get; set; } = new();
        public Dictionary<string, object> Metadata { get; set; } = new();
        public Dictionary<string, object> Configuration { get; set; } = new();

        // Navigation properties
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<FlowEntity> Flows { get; set; } = new List<FlowEntity>();
        public ICollection<TenantSetting> Settings { get; set; } = new List<TenantSetting>();
    }
}