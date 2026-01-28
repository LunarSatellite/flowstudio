using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Base
{
    /// <summary>
    /// Base entity with comprehensive audit fields, soft delete, concurrency control, and metadata
    /// </summary>
    [Index(nameof(CreatedAt))]
    [Index(nameof(IsDeleted))]
    [Index(nameof(CreatedAt), nameof(IsDeleted))]
    public abstract class BaseEntity
    {
        // Primary Key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        // Audit Trail - Creation
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string? CreatedBy { get; set; }
        
        public Guid? CreatedByUserId { get; set; }
        
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? CreatedByIpAddress { get; set; }
        
        [MaxLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string? CreatedByUserAgent { get; set; }
        
        // Audit Trail - Modification
        [Column(TypeName = "datetime2")]
        public DateTime? UpdatedAt { get; set; }
        
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string? UpdatedBy { get; set; }
        
        public Guid? UpdatedByUserId { get; set; }
        
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? UpdatedByIpAddress { get; set; }
        
        [MaxLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string? UpdatedByUserAgent { get; set; }
        
        public int UpdateCount { get; set; } = 0;
        
        // Soft Delete
        [Required]
        public bool IsDeleted { get; set; } = false;
        
        [Column(TypeName = "datetime2")]
        public DateTime? DeletedAt { get; set; }
        
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string? DeletedBy { get; set; }
        
        public Guid? DeletedByUserId { get; set; }
        
        [MaxLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string? DeletedReason { get; set; }
        
        // Concurrency Control
        [Timestamp]
        [Required]
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();
        
        [Required]
        public Guid ConcurrencyStamp { get; set; } = Guid.NewGuid();
        
        // Archiving & Retention
        public bool IsArchived { get; set; } = false;
        public DateTime? ArchivedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public DateTime? RetentionUntil { get; set; }
        
        // Status & State Management
        public bool IsActive { get; set; } = true;
        public bool IsLocked { get; set; } = false;
        public DateTime? LockedAt { get; set; }
        public string? LockedBy { get; set; }
        public string? LockedReason { get; set; }
        
        // Versioning
        public int Version { get; set; } = 1;
        public Guid? ParentId { get; set; }
        public string? VersionLabel { get; set; }
        public bool IsLatestVersion { get; set; } = true;
        
        // Data Quality & Validation
        public bool IsVerified { get; set; } = false;
        public DateTime? VerifiedAt { get; set; }
        public string? VerifiedBy { get; set; }
        public double? DataQualityScore { get; set; }
        public DateTime? LastValidatedAt { get; set; }
        
        // Search & Indexing
        public string? SearchVector { get; set; }
        public DateTime? LastIndexedAt { get; set; }
        public string? SearchKeywords { get; set; }
        
        // Multi-language Support
        public string? PrimaryLanguage { get; set; }
        public List<string> SupportedLanguages { get; set; } = new();
        
        // Geographic Information
        public string? Timezone { get; set; }
        public string? Region { get; set; }
        public string? Country { get; set; }
        
        // External Integration
        public string? ExternalId { get; set; }
        public string? ExternalSource { get; set; }
        public DateTime? ExternalSyncedAt { get; set; }
        public Dictionary<string, string> ExternalIds { get; set; } = new(); // Multiple external systems
        
        // Tags & Categorization
        public List<string> Tags { get; set; } = new();
        public List<string> Categories { get; set; } = new();
        
        // Custom Fields & Extensibility
        public Dictionary<string, object> Metadata { get; set; } = new();
        public Dictionary<string, object> CustomFields { get; set; } = new();
        public Dictionary<string, object> SystemFields { get; set; } = new();
        
        // Performance & Caching
        public string? CacheKey { get; set; }
        public DateTime? CachedAt { get; set; }
        public int? CacheDurationSeconds { get; set; }
        
        // Security & Compliance
        public bool IsEncrypted { get; set; } = false;
        public string? EncryptionKeyId { get; set; }
        public bool IsSensitive { get; set; } = false;
        public List<string> ComplianceTags { get; set; } = new(); // GDPR, HIPAA, PCI, etc.
        public bool RequiresAudit { get; set; } = true;
        
        // Change Tracking
        public string? ChangeReason { get; set; }
        public Dictionary<string, object> ChangeLog { get; set; } = new();
        public string? ApprovalStatus { get; set; }
        public Guid? ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }
        
        // Relationships & Hierarchy
        public int HierarchyLevel { get; set; } = 0;
        public string? HierarchyPath { get; set; }
        public Guid? OwnerId { get; set; }
        public string? OwnerType { get; set; }
        
        // Display & UI
        public int DisplayOrder { get; set; } = 0;
        public string? DisplayName { get; set; }
        public string? IconUrl { get; set; }
        public string? Color { get; set; }
        
        // Workflow & Process
        public string? WorkflowState { get; set; }
        public Guid? WorkflowInstanceId { get; set; }
        public DateTime? WorkflowStartedAt { get; set; }
        public DateTime? WorkflowCompletedAt { get; set; }
        
        // Notes & Comments
        public string? InternalNotes { get; set; }
        public int CommentCount { get; set; } = 0;
        public int AttachmentCount { get; set; } = 0;
        
        // Analytics & Tracking
        public long ViewCount { get; set; } = 0;
        public DateTime? LastViewedAt { get; set; }
        public long AccessCount { get; set; } = 0;
        public DateTime? LastAccessedAt { get; set; }
        
        // Feature Flags
        public Dictionary<string, bool> FeatureFlags { get; set; } = new();
        
        // Hash & Checksum (for integrity)
        public string? ContentHash { get; set; }
        public string? Checksum { get; set; }
        public string? ETag { get; set; }
    }
}
