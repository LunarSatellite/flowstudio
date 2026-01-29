using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aurora.FlowStudio.Entity.Base
{
    /// <summary>
    /// Optimized Base Entity
    /// 12 essential columns + 1 JSON metadata column = 13 total columns
    /// Much cleaner than 56 columns!
    /// </summary>
    public abstract class BaseEntity
    {
        #region Essential Columns (12) - Indexed for fast queries

        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public Guid? CreatedByUserId { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        public Guid? DeletedByUserId { get; set; }

        public bool IsActive { get; set; }

        public int Version { get; set; }

        public Guid ConcurrencyStamp { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }

        #endregion

        #region JSON Metadata Column

        [Column(TypeName = "jsonb")]
        public AuditMetadata Metadata { get; set; }

        #endregion

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            IsDeleted = false;
            IsActive = true;
            Version = 1;
            ConcurrencyStamp = Guid.NewGuid();
            Metadata = new AuditMetadata();
        }
    }

    /// <summary>
    /// Audit metadata - All fields that RepositoryBase needs in JSON
    /// </summary>
    public class AuditMetadata
    {
        // Creation Audit
        public string? CreatedBy { get; set; }
        public string? CreatedByIpAddress { get; set; }
        public string? CreatedByUserAgent { get; set; }

        // Update Audit
        public string? UpdatedBy { get; set; }
        public string? UpdatedByIpAddress { get; set; }
        public string? UpdatedByUserAgent { get; set; }
        public int UpdateCount { get; set; }

        // Delete Audit
        public string? DeletedBy { get; set; }
        public string? DeletedReason { get; set; }

        // Status & Workflow
        public bool IsLocked { get; set; }
        public DateTime? LockedAt { get; set; }
        public string? LockedBy { get; set; }
        public string? LockReason { get; set; }
        public bool IsArchived { get; set; }
        public DateTime? ArchivedAt { get; set; }
        public bool IsVerified { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string? VerifiedBy { get; set; }
        public double? DataQualityScore { get; set; }

        // Version Control
        public bool IsLatestVersion { get; set; }
        public string? VersionLabel { get; set; }
        public Guid? ParentId { get; set; }

        // Ownership
        public Guid? OwnerId { get; set; }
        public string? OwnerType { get; set; }

        // Tracking
        public int ViewCount { get; set; }
        public DateTime? LastViewedAt { get; set; }
        public int AccessCount { get; set; }
        public DateTime? LastAccessedAt { get; set; }
        public DateTime? LastValidatedAt { get; set; }
        public int CommentCount { get; set; }
        public int AttachmentCount { get; set; }

        // Geo & Locale
        public string? Country { get; set; }
        public string? Region { get; set; }
        public string? Timezone { get; set; }
        public string? PrimaryLanguage { get; set; }
        public List<string> SupportedLanguages { get; set; }

        // Cache & Performance
        public string? CacheKey { get; set; }
        public DateTime? CacheExpiresAt { get; set; }
        public DateTime? CachedAt { get; set; }
        public int CacheDurationSeconds { get; set; }
        public bool RequiresCacheRefresh { get; set; }
        public string? ContentHash { get; set; }
        public string? Checksum { get; set; }
        public string? ETag { get; set; }

        // Additional
        public bool RequiresAudit { get; set; }
        public int DisplayOrder { get; set; }

        public AuditMetadata()
        {
            UpdateCount = 0;
            ViewCount = 0;
            AccessCount = 0;
            CommentCount = 0;
            AttachmentCount = 0;
            IsLatestVersion = true;
            RequiresAudit = true;
            RequiresCacheRefresh = false;
            CacheDurationSeconds = 0;
            DisplayOrder = 0;
            SupportedLanguages = new List<string>();
        }
    }
}