using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Aurora.FlowStudio.Data.Context;
using Aurora.FlowStudio.Entity.Base;
using Aurora.FlowStudio.Infrastructure.OData;
using Aurora.FlowStudio.Infrastructure.Results;
using Aurora.FlowStudio.Infrastructure.Services;
using EFCore.BulkExtensions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Security.Cryptography;
using System.Text;

namespace Aurora.FlowStudio.Infrastructure.Implementations;

/// </summary>
public class RepositoryBase<TEntity, TDto> : IRepository<TEntity, TDto>
    where TEntity : BaseEntity
    where TDto : class
{
    protected readonly IDatabaseFactory _databaseFactory;
    protected readonly IMemoryCache _memoryCache;
    protected readonly ILogger _logger;
    protected readonly IDistributedCache? _distributedCache;
    protected readonly ICurrentUserService? _currentUserService;
    protected FlowStudioDbContext Context => _databaseFactory.Get();

    private readonly TimeSpan _defaultCacheExpiration = TimeSpan.FromMinutes(15);
    private readonly string _cacheKeyPrefix;

    public RepositoryBase(
        IDatabaseFactory databaseFactory,
        IMemoryCache memoryCache,
        ILogger logger,
        IDistributedCache? distributedCache = null,
        ICurrentUserService? currentUserService = null)
    {
        _databaseFactory = databaseFactory ?? throw new ArgumentNullException(nameof(databaseFactory));
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _distributedCache = distributedCache;
        _currentUserService = currentUserService;
        _cacheKeyPrefix = $"{typeof(TEntity).Name}:";

        _logger.LogDebug("RepositoryBase<{EntityType}> instance created", typeof(TEntity).Name);
    }

    #region Comprehensive Audit Field Helpers

    /// <summary>
    /// ✅ AUTO: Sets ALL creation audit fields (25+ fields) + Cache + Hash + Quality
    /// </summary>
    private void SetCreatedAuditFields(TEntity entity)
    {
        if (entity.Metadata == null)
        {
            entity.Metadata = new AuditMetadata();
        }

        var now = DateTime.UtcNow;

        entity.CreatedAt = now;
        entity.IsDeleted = false;
        entity.Metadata.UpdateCount = 0;
        entity.IsActive = true;
        entity.Metadata.IsLocked = false;
        entity.Version = 1;
        entity.Metadata.IsLatestVersion = true;
        entity.ConcurrencyStamp = Guid.NewGuid();
        entity.Metadata.RequiresAudit = true;
        entity.Metadata.DisplayOrder = 0;
        entity.Metadata.ViewCount = 0;
        entity.Metadata.AccessCount = 0;
        entity.Metadata.CommentCount = 0;
        entity.Metadata.AttachmentCount = 0;
        entity.Metadata.IsArchived = false;
        entity.Metadata.IsVerified = false;

        if (_currentUserService != null)
        {
            var userId = _currentUserService.GetUserId();
            var userName = _currentUserService.GetUserName();
            var ipAddress = _currentUserService.GetIpAddress();
            var userAgent = _currentUserService.GetUserAgent();
            var timezone = _currentUserService.GetTimezone();
            var country = _currentUserService.GetCountry();
            var language = _currentUserService.GetLanguage();

            if (userId.HasValue)
            {
                entity.CreatedByUserId = userId.Value;
                entity.Metadata.OwnerId = userId.Value;
            }

            if (!string.IsNullOrEmpty(userName))
            {
                entity.Metadata.CreatedBy = userName;
            }

            if (!string.IsNullOrEmpty(ipAddress))
            {
                entity.Metadata.CreatedByIpAddress = ipAddress.Length > 50 ? ipAddress.Substring(0, 50) : ipAddress;
            }

            if (!string.IsNullOrEmpty(userAgent))
            {
                entity.Metadata.CreatedByUserAgent = userAgent.Length > 500 ? userAgent.Substring(0, 500) : userAgent;
            }

            if (!string.IsNullOrEmpty(timezone))
            {
                entity.Metadata.Timezone = timezone;
            }

            if (!string.IsNullOrEmpty(country))
            {
                entity.Metadata.Country = country;
                entity.Metadata.Region = country;
            }

            if (!string.IsNullOrEmpty(language))
            {
                entity.Metadata.PrimaryLanguage = language;
                if (!entity.Metadata.SupportedLanguages.Contains(language))
                {
                    entity.Metadata.SupportedLanguages.Add(language);
                }
            }
        }

        // ✅ AUTO-CALL: Update cache info
        UpdateCacheInfo(entity);

        // ✅ AUTO-CALL: Update content hash
        UpdateContentHash(entity);

        // ✅ AUTO-CALL: Auto-verify data quality
        AutoVerifyDataQuality(entity);

        _logger.LogDebug("Created audit: {User} from {IP}", entity.Metadata.CreatedBy, entity.Metadata.CreatedByIpAddress);
    }

    /// <summary>
    /// ✅ AUTO: Sets ALL update audit fields (12+ fields) + Cache + Hash + Quality
    /// </summary>
    private void SetUpdatedAuditFields(TEntity entity)
    {
        if (entity.Metadata == null)
        {
            entity.Metadata = new AuditMetadata();
        }

        var now = DateTime.UtcNow;

        entity.UpdatedAt = now;
        entity.Metadata.UpdateCount++;
        entity.ConcurrencyStamp = Guid.NewGuid();
        entity.Metadata.LastValidatedAt = now;
        entity.Metadata.LastAccessedAt = now;
        entity.Metadata.AccessCount++;

        if (_currentUserService != null)
        {
            var userId = _currentUserService.GetUserId();
            var userName = _currentUserService.GetUserName();
            var ipAddress = _currentUserService.GetIpAddress();
            var userAgent = _currentUserService.GetUserAgent();

            if (userId.HasValue)
            {
                entity.UpdatedByUserId = userId.Value;
            }

            if (!string.IsNullOrEmpty(userName))
            {
                entity.Metadata.UpdatedBy = userName;
            }

            if (!string.IsNullOrEmpty(ipAddress))
            {
                entity.Metadata.UpdatedByIpAddress = ipAddress.Length > 50 ? ipAddress.Substring(0, 50) : ipAddress;
            }

            if (!string.IsNullOrEmpty(userAgent))
            {
                entity.Metadata.UpdatedByUserAgent = userAgent.Length > 500 ? userAgent.Substring(0, 500) : userAgent;
            }
        }

        // ✅ AUTO-CALL: Update cache info
        UpdateCacheInfo(entity);

        // ✅ AUTO-CALL: Update content hash
        UpdateContentHash(entity);

        // ✅ AUTO-CALL: Auto-verify data quality
        AutoVerifyDataQuality(entity);

        _logger.LogDebug("Updated audit: {User} (count: {Count})", entity.Metadata.UpdatedBy, entity.Metadata.UpdateCount);
    }

    /// <summary>
    /// ✅ SET ALL DELETED AUDIT FIELDS (5+ fields)
    /// </summary>
    private void SetDeletedAuditFields(TEntity entity, Guid? DeletedByUserId = null, string? deleteReason = null)
    {
        if (entity.Metadata == null)
        {
            entity.Metadata = new AuditMetadata();
        }

        var now = DateTime.UtcNow;

        entity.IsDeleted = true;
        entity.DeletedAt = now;
        entity.Metadata.DeletedReason = deleteReason;
        entity.IsActive = false;

        if (DeletedByUserId.HasValue)
        {
            entity.DeletedByUserId = DeletedByUserId.Value;
            entity.Metadata.DeletedBy = $"User:{DeletedByUserId.Value}";
        }
        else if (_currentUserService != null)
        {
            var userId = _currentUserService.GetUserId();
            var userName = _currentUserService.GetUserName();

            if (userId.HasValue)
            {
                entity.DeletedByUserId = userId.Value;
            }

            if (!string.IsNullOrEmpty(userName))
            {
                entity.Metadata.DeletedBy = userName;
            }
        }

        _logger.LogDebug("Deleted audit: {User}, Reason: {Reason}", entity.DeletedByUserId, entity.Metadata.DeletedReason);
    }

    /// <summary>
    /// ✅ INCREMENT ACCESS TRACKING
    /// </summary>
    private void IncrementAccessTracking(TEntity entity)
    {
        if (entity.Metadata == null)
        {
            entity.Metadata = new AuditMetadata();
        }

        var now = DateTime.UtcNow;
        entity.Metadata.AccessCount++;
        entity.Metadata.LastAccessedAt = now;
    }

    /// <summary>
    /// ✅ INCREMENT VIEW TRACKING
    /// </summary>
    private void IncrementViewTracking(TEntity entity)
    {
        if (entity.Metadata == null)
        {
            entity.Metadata = new AuditMetadata();
        }

        var now = DateTime.UtcNow;
        entity.Metadata.ViewCount++;
        entity.Metadata.LastViewedAt = now;
    }

    /// <summary>
    /// ✅ SET ARCHIVING FIELDS
    /// </summary>
    private void SetArchivedFields(TEntity entity, Guid? archivedByUserId = null)
    {
        if (entity.Metadata == null)
        {
            entity.Metadata = new AuditMetadata();
        }

        var now = DateTime.UtcNow;
        entity.Metadata.IsArchived = true;
        entity.Metadata.ArchivedAt = now;
        entity.IsActive = false;
        SetUpdatedAuditFields(entity);
        if (archivedByUserId.HasValue)
        {
            entity.UpdatedByUserId = archivedByUserId.Value;
        }
    }

    /// <summary>
    /// ✅ SET LOCKING FIELDS
    /// </summary>
    private void SetLockedFields(TEntity entity, string? lockReason = null)
    {
        if (entity.Metadata == null)
        {
            entity.Metadata = new AuditMetadata();
        }

        var now = DateTime.UtcNow;
        entity.Metadata.IsLocked = true;
        entity.Metadata.LockedAt = now;
        entity.Metadata.LockReason = lockReason;

        if (_currentUserService != null)
        {
            var userName = _currentUserService.GetUserName();
            if (!string.IsNullOrEmpty(userName))
            {
                entity.Metadata.LockedBy = userName;
            }
        }
    }

    /// <summary>
    /// ✅ UNLOCK ENTITY
    /// </summary>
    private void UnlockEntity(TEntity entity)
    {
        if (entity.Metadata == null)
        {
            entity.Metadata = new AuditMetadata();
        }

        entity.Metadata.IsLocked = false;
        entity.Metadata.LockedAt = null;
        entity.Metadata.LockedBy = null;
        entity.Metadata.LockReason = null;
    }

    /// <summary>
    /// ✅ SET VERIFICATION FIELDS
    /// </summary>
    private void SetVerifiedFields(TEntity entity, double? dataQualityScore = null)
    {
        if (entity.Metadata == null)
        {
            entity.Metadata = new AuditMetadata();
        }

        var now = DateTime.UtcNow;
        entity.Metadata.IsVerified = true;
        entity.Metadata.VerifiedAt = now;
        entity.Metadata.LastValidatedAt = now;

        if (dataQualityScore.HasValue)
        {
            entity.Metadata.DataQualityScore = dataQualityScore.Value;
        }

        if (_currentUserService != null)
        {
            var userName = _currentUserService.GetUserName();
            if (!string.IsNullOrEmpty(userName))
            {
                entity.Metadata.VerifiedBy = userName;
            }
        }
    }

    /// <summary>
    /// ✅ INCREMENT VERSION
    /// </summary>
    private void IncrementVersion(TEntity entity, string? versionLabel = null)
    {
        if (entity.Metadata == null)
        {
            entity.Metadata = new AuditMetadata();
        }

        entity.Version++;
        entity.Metadata.VersionLabel = versionLabel ?? $"v{entity.Version}";
        entity.ConcurrencyStamp = Guid.NewGuid();
        SetUpdatedAuditFields(entity);
    }

    /// <summary>
    /// ✅ UPDATE CACHE INFO
    /// </summary>
    private void UpdateCacheInfo(TEntity entity, int? cacheDurationSeconds = null)
    {
        if (entity.Metadata == null)
        {
            entity.Metadata = new AuditMetadata();
        }

        var now = DateTime.UtcNow;
        entity.Metadata.CachedAt = now;
        entity.Metadata.CacheDurationSeconds = cacheDurationSeconds ?? 900;
        entity.Metadata.CacheKey = $"{typeof(TEntity).Name}:{entity.Id}";
    }

    /// <summary>
    /// ✅ UPDATE CONTENT HASH
    /// </summary>
    private void UpdateContentHash(TEntity entity)
    {
        if (entity.Metadata == null)
        {
            entity.Metadata = new AuditMetadata();
        }

        try
        {
            var content = JsonSerializer.Serialize(entity);
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(content));
            entity.Metadata.ContentHash = Convert.ToBase64String(hashBytes);
            entity.Metadata.Checksum = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            entity.Metadata.ETag = $"\"{entity.Metadata.ContentHash}\"";
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to update content hash for {EntityType}", typeof(TEntity).Name);
        }
    }

    /// <summary>
    /// ✅ AUTO-VERIFY DATA QUALITY (Auto-calculate quality score)
    /// </summary>
    private void AutoVerifyDataQuality(TEntity entity)
    {
        if (entity.Metadata == null)
        {
            entity.Metadata = new AuditMetadata();
        }

        try
        {
            // Simple data quality scoring based on completeness
            int score = 0;
            int totalFields = 0;

            var properties = typeof(TEntity).GetProperties()
                .Where(p => p.CanRead && p.PropertyType != typeof(byte[]) && !p.Name.StartsWith("Navigation"));

            foreach (var prop in properties)
            {
                totalFields++;
                var value = prop.GetValue(entity);
                if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
                {
                    score++;
                }
            }

            if (totalFields > 0)
            {
                entity.Metadata.DataQualityScore = (double)score / totalFields * 100;

                // Auto-verify if quality is high enough
                if (entity.Metadata.DataQualityScore >= 80.0)
                {
                    entity.Metadata.IsVerified = true;
                    entity.Metadata.VerifiedAt = DateTime.UtcNow;

                    if (_currentUserService != null)
                    {
                        var userName = _currentUserService.GetUserName();
                        if (!string.IsNullOrEmpty(userName))
                        {
                            entity.Metadata.VerifiedBy = userName;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to auto-verify data quality for {EntityType}", typeof(TEntity).Name);
        }
    }

    #endregion

    #region Query Operations

    public virtual async Task<TDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Getting {EntityType} by ID: {Id}", typeof(TEntity).Name, id);

            // ✅ AUTO: Get entity with view tracking
            var entity = await Context.Set<TEntity>()
                .Where(e => e.Id!.Equals(id) && !e.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity != null)
            {
                // ✅ AUTO-CALL: Increment view tracking
                IncrementViewTracking(entity);

                // Save view count immediately
                await Context.SaveChangesAsync(cancellationToken);

                _logger.LogDebug("{EntityType} with ID {Id} found (ViewCount: {ViewCount})",
                    typeof(TEntity).Name, id, entity.Metadata.ViewCount);

                return entity.Adapt<TDto>();
            }
            else
            {
                _logger.LogDebug("{EntityType} with ID {Id} not found", typeof(TEntity).Name, id);
                return null;
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("GetByIdAsync cancelled for {EntityType} ID: {Id}", typeof(TEntity).Name, id);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting {EntityType} by ID: {Id}", typeof(TEntity).Name, id);
            throw;
        }
    }

    public virtual async Task<TDto?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes)
    {
        try
        {
            _logger.LogDebug("Getting {EntityType} by ID with {IncludeCount} includes: {Id}",
                typeof(TEntity).Name, includes.Length, id);

            var query = Context.Set<TEntity>()
                .Where(e => e.Id!.Equals(id) && !e.IsDeleted);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var entity = await query.FirstOrDefaultAsync();

            if (entity != null)
            {
                // ✅ AUTO-CALL: Increment view tracking
                IncrementViewTracking(entity);

                // Save view count immediately
                await Context.SaveChangesAsync();

                _logger.LogDebug("{EntityType} with ID {Id} retrieved with view tracking", typeof(TEntity).Name, id);
                return entity.Adapt<TDto>();
            }

            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting {EntityType} by ID with includes: {Id}", typeof(TEntity).Name, id);
            throw;
        }
    }

    public virtual async Task<List<TDto>> GetAllAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Getting all {EntityType} entities", typeof(TEntity).Name);

            var query = Context.Set<TEntity>()
                .AsNoTracking()
                .Where(e => !e.IsDeleted);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var result = await query
                .ProjectToType<TDto>()
                .ToListAsync(cancellationToken);

            _logger.LogDebug("Retrieved {Count} {EntityType} entities", result.Count, typeof(TEntity).Name);
            return result;
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("GetAllAsync cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all {EntityType} entities", typeof(TEntity).Name);
            throw;
        }
    }

    public virtual async Task<List<TDto>> FindAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Finding {EntityType} entities with predicate", typeof(TEntity).Name);

            var result = await Context.Set<TEntity>()
                .AsNoTracking()
                .Where(e => !e.IsDeleted)
                .Where(predicate)
                .ProjectToType<TDto>()
                .ToListAsync(cancellationToken);

            _logger.LogDebug("Found {Count} {EntityType} entities", result.Count, typeof(TEntity).Name);
            return result;
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("FindAsync cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error finding {EntityType} entities", typeof(TEntity).Name);
            throw;
        }
    }

    public virtual async Task<TDto?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Getting first or default {EntityType}", typeof(TEntity).Name);

            var result = await Context.Set<TEntity>()
                .AsNoTracking()
                .Where(e => !e.IsDeleted)
                .Where(predicate)
                .ProjectToType<TDto>()
                .FirstOrDefaultAsync(cancellationToken);

            _logger.LogDebug("FirstOrDefault {EntityType} completed", typeof(TEntity).Name);
            return result;
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("FirstOrDefaultAsync cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting first or default {EntityType}", typeof(TEntity).Name);
            throw;
        }
    }

    public virtual async Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Checking {EntityType} existence", typeof(TEntity).Name);

            var exists = await Context.Set<TEntity>()
                .AsNoTracking()
                .Where(e => !e.IsDeleted)
                .AnyAsync(predicate, cancellationToken);

            _logger.LogDebug("{EntityType} exists: {Exists}", typeof(TEntity).Name, exists);
            return exists;
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("ExistsAsync cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking {EntityType} existence", typeof(TEntity).Name);
            throw;
        }
    }

    public virtual async Task<int> CountAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Counting {EntityType} entities", typeof(TEntity).Name);

            var query = Context.Set<TEntity>()
                .AsNoTracking()
                .Where(e => !e.IsDeleted);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var count = await query.CountAsync(cancellationToken);
            _logger.LogDebug("{EntityType} count: {Count}", typeof(TEntity).Name, count);
            return count;
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("CountAsync cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error counting {EntityType} entities", typeof(TEntity).Name);
            throw;
        }
    }

    public virtual async Task<long> LongCountAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Long counting {EntityType} entities", typeof(TEntity).Name);

            var query = Context.Set<TEntity>()
                .AsNoTracking()
                .Where(e => !e.IsDeleted);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var count = await query.LongCountAsync(cancellationToken);
            _logger.LogDebug("{EntityType} long count: {Count}", typeof(TEntity).Name, count);
            return count;
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("LongCountAsync cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error long counting {EntityType} entities", typeof(TEntity).Name);
            throw;
        }
    }

    #endregion

    #region OData Query Operations

    public virtual async Task<ODataPagedResult<TDto>> QueryAsync(
        ODataQueryOptions options,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Executing OData query for {EntityType}", typeof(TEntity).Name);

            var baseQuery = Context.Set<TEntity>()
                .AsNoTracking()
                .Where(e => !e.IsDeleted);

            var result = await new ODataQueryHandler<TEntity>(
                Microsoft.Extensions.Logging.Abstractions.NullLogger<ODataQueryHandler<TEntity>>.Instance
            ).ExecuteQueryAsync(baseQuery, options, cancellationToken);

            var dtos = result.Value.Adapt<List<TDto>>();

            _logger.LogDebug("OData query returned {Count} {EntityType} entities",
                dtos.Count, typeof(TEntity).Name);

            return new ODataPagedResult<TDto>
            {
                Value = dtos,
                Count = result.Count,
                CurrentPage = result.CurrentPage,
                PageSize = result.PageSize,
                NextLink = result.NextLink,
                PrevLink = result.PrevLink
            };
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("OData QueryAsync cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing OData query for {EntityType}", typeof(TEntity).Name);
            throw;
        }
    }

    public virtual async Task<ODataPagedResult<TDto>> QueryAsync(
        Expression<Func<TEntity, bool>> baseFilter,
        ODataQueryOptions options,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Executing OData query with filter for {EntityType}", typeof(TEntity).Name);

            var baseQuery = Context.Set<TEntity>()
                .AsNoTracking()
                .Where(e => !e.IsDeleted)
                .Where(baseFilter);

            var result = await new ODataQueryHandler<TEntity>(
                Microsoft.Extensions.Logging.Abstractions.NullLogger<ODataQueryHandler<TEntity>>.Instance
            ).ExecuteQueryAsync(baseQuery, options, cancellationToken);

            var dtos = result.Value.Adapt<List<TDto>>();

            _logger.LogDebug("OData query with filter returned {Count} {EntityType} entities",
                dtos.Count, typeof(TEntity).Name);

            return new ODataPagedResult<TDto>
            {
                Value = dtos,
                Count = result.Count,
                CurrentPage = result.CurrentPage,
                PageSize = result.PageSize,
                NextLink = result.NextLink,
                PrevLink = result.PrevLink
            };
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("OData QueryAsync with filter cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing OData query with filter for {EntityType}", typeof(TEntity).Name);
            throw;
        }
    }

    public virtual async Task<ODataPagedResult<TResult>> QueryAsync<TResult>(
        ODataQueryOptions options,
        CancellationToken cancellationToken = default) where TResult : class
    {
        try
        {
            _logger.LogDebug("Executing OData query with custom projection for {EntityType}", typeof(TEntity).Name);

            var baseQuery = Context.Set<TEntity>()
                .AsNoTracking()
                .Where(e => !e.IsDeleted);

            var result = await new ODataQueryHandler<TEntity>(
                Microsoft.Extensions.Logging.Abstractions.NullLogger<ODataQueryHandler<TEntity>>.Instance
            ).ExecuteQueryAsync<TResult>(baseQuery, options, cancellationToken);

            _logger.LogDebug("OData query with projection returned {Count} results", result.Value.Count);

            return new ODataPagedResult<TResult>
            {
                Value = result.Value,
                Count = result.Count,
                CurrentPage = result.CurrentPage,
                PageSize = result.PageSize,
                NextLink = result.NextLink,
                PrevLink = result.PrevLink
            };
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("OData QueryAsync with projection cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing OData query with projection for {EntityType}", typeof(TEntity).Name);
            throw;
        }
    }

    #endregion

    #region Paging Operations

    public virtual async Task<(List<TDto> Items, int TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Getting paged {EntityType}: Page {PageNumber}, Size {PageSize}",
                typeof(TEntity).Name, pageNumber, pageSize);

            var query = Context.Set<TEntity>()
                .AsNoTracking()
                .Where(e => !e.IsDeleted);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            IQueryable<TEntity> orderedQuery = orderBy != null
                ? orderBy(query)
                : query.OrderBy(e => e.CreatedAt);

            var items = await orderedQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ProjectToType<TDto>()
                .ToListAsync(cancellationToken);

            _logger.LogDebug("Retrieved {ItemCount}/{TotalCount} {EntityType} entities",
                items.Count, totalCount, typeof(TEntity).Name);

            return (items, totalCount);
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("GetPagedAsync cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting paged {EntityType}", typeof(TEntity).Name);
            throw;
        }
    }

    public virtual async Task<(List<TDto> Items, Guid? NextCursor)> GetPagedByCursorAsync(
      Guid? cursor,
      int pageSize,
      Expression<Func<TEntity, bool>>? filter = null,
      CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Getting paged {EntityType} by cursor, Size {PageSize}",
                typeof(TEntity).Name, pageSize);

            var query = Context.Set<TEntity>()
                .AsNoTracking()
                .Where(e => !e.IsDeleted);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (cursor != null)
            {
                query = query.Where(e => Comparer<Guid>.Default.Compare(e.Id, cursor.Value) > 0);
            }

            var items = await query
                .OrderBy(e => e.Id)
                .Take(pageSize + 1)
                .ProjectToType<TDto>()
                .ToListAsync(cancellationToken);

            Guid? nextCursor = default;
            if (items.Count > pageSize)
            {
                var lastItem = items[pageSize];
                var idProperty = typeof(TDto).GetProperty("Id");
                if (idProperty != null)
                {
                    nextCursor = (Guid?)idProperty.GetValue(lastItem);
                }
                items = items.Take(pageSize).ToList();
            }

            _logger.LogDebug("Retrieved {Count} {EntityType} entities by cursor",
                items.Count, typeof(TEntity).Name);

            return (items, nextCursor);
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("GetPagedByCursorAsync cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting paged {EntityType} by cursor", typeof(TEntity).Name);
            throw;
        }
    }

    #endregion

    #region Command Operations with Comprehensive Audit

    public virtual async Task<TDto> AddAsync(TDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Adding new {EntityType}", typeof(TEntity).Name);

            var entity = dto.Adapt<TEntity>();
            SetCreatedAuditFields(entity);

            await Context.Set<TEntity>().AddAsync(entity, cancellationToken);

            _logger.LogDebug("New {EntityType} added with audit", typeof(TEntity).Name);
            return entity.Adapt<TDto>();
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("AddAsync cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding {EntityType}", typeof(TEntity).Name);
            throw;
        }
    }

    public virtual async Task<List<TDto>> AddRangeAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default)
    {
        try
        {
            var dtoList = dtos.ToList();
            _logger.LogDebug("Adding {Count} {EntityType} entities", dtoList.Count, typeof(TEntity).Name);

            var entities = dtoList.Adapt<List<TEntity>>();

            foreach (var entity in entities)
            {
                SetCreatedAuditFields(entity);
            }

            await Context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);

            _logger.LogDebug("Added {Count} {EntityType} entities with audit", entities.Count, typeof(TEntity).Name);
            return entities.Adapt<List<TDto>>();
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("AddRangeAsync cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding range of {EntityType}", typeof(TEntity).Name);
            throw;
        }
    }

    public virtual async Task<TDto> UpdateAsync(TDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Updating {EntityType}", typeof(TEntity).Name);

            var entity = dto.Adapt<TEntity>();
            SetUpdatedAuditFields(entity);

            Context.Set<TEntity>().Update(entity);

            await InvalidateCacheAsync(entity.Id);

            _logger.LogDebug("{EntityType} updated with audit", typeof(TEntity).Name);
            return entity.Adapt<TDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating {EntityType}", typeof(TEntity).Name);
            throw;
        }
    }

    public virtual async Task UpdateRangeAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default)
    {
        try
        {
            var dtoList = dtos.ToList();
            _logger.LogDebug("Updating {Count} {EntityType} entities", dtoList.Count, typeof(TEntity).Name);

            var entities = dtoList.Adapt<List<TEntity>>();

            foreach (var entity in entities)
            {
                SetUpdatedAuditFields(entity);
            }

            Context.Set<TEntity>().UpdateRange(entities);

            foreach (var entity in entities)
            {
                await InvalidateCacheAsync(entity.Id);
            }

            _logger.LogDebug("Updated {Count} {EntityType} entities with audit", entities.Count, typeof(TEntity).Name);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating range of {EntityType}", typeof(TEntity).Name);
            throw;
        }
    }

    public virtual async Task<TDto> PatchAsync(Guid id, Dictionary<string, object> updates, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Patching {EntityType} ID {Id} with {UpdateCount} updates",
                typeof(TEntity).Name, id, updates.Count);

            var entity = await Context.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id!.Equals(id) && !e.IsDeleted, cancellationToken);

            if (entity == null)
            {
                _logger.LogWarning("{EntityType} with ID {Id} not found for patching", typeof(TEntity).Name, id);
                throw new KeyNotFoundException($"{typeof(TEntity).Name} with ID {id} not found");
            }

            var entityType = typeof(TEntity);
            foreach (var (key, value) in updates)
            {
                var property = entityType.GetProperty(key);
                if (property != null && property.CanWrite)
                {
                    property.SetValue(entity, value);
                }
            }

            SetUpdatedAuditFields(entity);

            await InvalidateCacheAsync(id);

            _logger.LogDebug("{EntityType} ID {Id} patched with audit", typeof(TEntity).Name, id);
            return entity.Adapt<TDto>();
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("PatchAsync cancelled for {EntityType} ID {Id}", typeof(TEntity).Name, id);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error patching {EntityType} ID {Id}", typeof(TEntity).Name, id);
            throw;
        }
    }

    public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Deleting {EntityType} ID {Id}", typeof(TEntity).Name, id);

            var entity = await Context.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id!.Equals(id), cancellationToken);

            if (entity != null)
            {
                Context.Set<TEntity>().Remove(entity);

                await InvalidateCacheAsync(id);
                _logger.LogDebug("{EntityType} ID {Id} deleted", typeof(TEntity).Name, id);
            }
            else
            {
                _logger.LogWarning("{EntityType} ID {Id} not found for deletion", typeof(TEntity).Name, id);
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("DeleteAsync cancelled for {EntityType} ID {Id}", typeof(TEntity).Name, id);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting {EntityType} ID {Id}", typeof(TEntity).Name, id);
            throw;
        }
    }

    public virtual async Task DeleteRangeAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        try
        {
            var idList = ids.ToList();
            _logger.LogDebug("Deleting {Count} {EntityType} entities", idList.Count, typeof(TEntity).Name);

            var entities = await Context.Set<TEntity>()
                .Where(e => idList.Contains(e.Id))
                .ToListAsync(cancellationToken);

            Context.Set<TEntity>().RemoveRange(entities);

            foreach (var id in idList)
            {
                await InvalidateCacheAsync(id);
            }

            _logger.LogDebug("Deleted {Count} {EntityType} entities", entities.Count, typeof(TEntity).Name);
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("DeleteRangeAsync cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting range of {EntityType}", typeof(TEntity).Name);
            throw;
        }
    }

    #endregion

    #region Soft Delete Operations

    public virtual async Task SoftDeleteAsync(Guid id, Guid? DeletedByUserId = null, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Soft deleting {EntityType} ID {Id}", typeof(TEntity).Name, id);

            var entity = await Context.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id!.Equals(id) && !e.IsDeleted, cancellationToken);

            if (entity != null)
            {
                SetDeletedAuditFields(entity, DeletedByUserId);

                await InvalidateCacheAsync(id);
                _logger.LogDebug("{EntityType} ID {Id} soft deleted with audit", typeof(TEntity).Name, id);
            }
            else
            {
                _logger.LogWarning("{EntityType} ID {Id} not found for soft deletion", typeof(TEntity).Name, id);
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("SoftDeleteAsync cancelled for {EntityType} ID {Id}", typeof(TEntity).Name, id);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error soft deleting {EntityType} ID {Id}", typeof(TEntity).Name, id);
            throw;
        }
    }

    public virtual async Task SoftDeleteRangeAsync(IEnumerable<Guid> ids, Guid? DeletedByUserId = null, CancellationToken cancellationToken = default)
    {
        try
        {
            var idList = ids.ToList();
            _logger.LogDebug("Soft deleting {Count} {EntityType} entities", idList.Count, typeof(TEntity).Name);

            var entities = await Context.Set<TEntity>()
                .Where(e => idList.Contains(e.Id) && !e.IsDeleted)
                .ToListAsync(cancellationToken);

            foreach (var entity in entities)
            {
                SetDeletedAuditFields(entity, DeletedByUserId);
            }

            foreach (var id in idList)
            {
                await InvalidateCacheAsync(id);
            }

            _logger.LogDebug("Soft deleted {Count} {EntityType} entities with audit", entities.Count, typeof(TEntity).Name);
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("SoftDeleteRangeAsync cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error soft deleting range of {EntityType}", typeof(TEntity).Name);
            throw;
        }
    }

    public virtual async Task RestoreAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Restoring {EntityType} ID {Id}", typeof(TEntity).Name, id);

            var entity = await Context.Set<TEntity>()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(e => e.Id!.Equals(id) && e.IsDeleted, cancellationToken);

            if (entity != null)
            {
                entity.IsDeleted = false;
                entity.DeletedAt = null;
                entity.DeletedByUserId = null;
                entity.Metadata.DeletedReason = null;

                SetUpdatedAuditFields(entity);

                await InvalidateCacheAsync(id);
                _logger.LogDebug("{EntityType} ID {Id} restored with audit", typeof(TEntity).Name, id);
            }
            else
            {
                _logger.LogWarning("{EntityType} ID {Id} not found for restoration", typeof(TEntity).Name, id);
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("RestoreAsync cancelled for {EntityType} ID {Id}", typeof(TEntity).Name, id);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error restoring {EntityType} ID {Id}", typeof(TEntity).Name, id);
            throw;
        }
    }

    public virtual async Task<List<TDto>> GetDeletedAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Getting deleted {EntityType} entities", typeof(TEntity).Name);

            var query = Context.Set<TEntity>()
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Where(e => e.IsDeleted);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var result = await query
                .ProjectToType<TDto>()
                .ToListAsync(cancellationToken);

            _logger.LogDebug("Retrieved {Count} deleted {EntityType} entities", result.Count, typeof(TEntity).Name);
            return result;
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("GetDeletedAsync cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting deleted {EntityType} entities", typeof(TEntity).Name);
            throw;
        }
    }

    #endregion

    #region Bulk Operations

    public virtual async Task<int> BulkInsertAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default)
    {
        try
        {
            var dtoList = dtos.ToList();
            _logger.LogInformation("Bulk inserting {Count} {EntityType} entities", dtoList.Count, typeof(TEntity).Name);

            var entities = dtoList.Adapt<List<TEntity>>();

            foreach (var entity in entities)
            {
                SetCreatedAuditFields(entity);
            }

            await Context.BulkInsertAsync(entities, cancellationToken: cancellationToken);

            _logger.LogInformation("Bulk inserted {Count} {EntityType} entities with audit", entities.Count, typeof(TEntity).Name);
            return entities.Count;
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("BulkInsertAsync cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error bulk inserting {EntityType} entities", typeof(TEntity).Name);
            throw;
        }
    }

    public virtual async Task<int> BulkUpdateAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default)
    {
        try
        {
            var dtoList = dtos.ToList();
            _logger.LogInformation("Bulk updating {Count} {EntityType} entities", dtoList.Count, typeof(TEntity).Name);

            var entities = dtoList.Adapt<List<TEntity>>();

            foreach (var entity in entities)
            {
                SetUpdatedAuditFields(entity);
            }

            await Context.BulkUpdateAsync(entities, cancellationToken: cancellationToken);
            await InvalidateAllCacheAsync();

            _logger.LogInformation("Bulk updated {Count} {EntityType} entities with audit", entities.Count, typeof(TEntity).Name);
            return entities.Count;
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("BulkUpdateAsync cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error bulk updating {EntityType} entities", typeof(TEntity).Name);
            throw;
        }
    }

    public virtual async Task<int> BulkDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Bulk deleting {EntityType} entities", typeof(TEntity).Name);

            var entities = await Context.Set<TEntity>()
                .Where(predicate)
                .ToListAsync(cancellationToken);

            await Context.BulkDeleteAsync(entities, cancellationToken: cancellationToken);
            await InvalidateAllCacheAsync();

            _logger.LogInformation("Bulk deleted {Count} {EntityType} entities", entities.Count, typeof(TEntity).Name);
            return entities.Count;
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("BulkDeleteAsync cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error bulk deleting {EntityType} entities", typeof(TEntity).Name);
            throw;
        }
    }

    public virtual async Task<int> BulkUpsertAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default)
    {
        try
        {
            var dtoList = dtos.ToList();
            _logger.LogInformation("Bulk upserting {Count} {EntityType} entities", dtoList.Count, typeof(TEntity).Name);

            var entities = dtoList.Adapt<List<TEntity>>();

            foreach (var entity in entities)
            {
                SetUpdatedAuditFields(entity);
                if (entity.CreatedAt == default)
                {
                    SetCreatedAuditFields(entity);
                }
            }

            await Context.BulkInsertOrUpdateAsync(entities, cancellationToken: cancellationToken);
            await InvalidateAllCacheAsync();

            _logger.LogInformation("Bulk upserted {Count} {EntityType} entities with audit", entities.Count, typeof(TEntity).Name);
            return entities.Count;
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("BulkUpsertAsync cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error bulk upserting {EntityType} entities", typeof(TEntity).Name);
            throw;
        }
    }

    #endregion

    #region Specification Pattern

    public virtual async Task<List<TDto>> GetAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Getting {EntityType} using specification", typeof(TEntity).Name);

            var result = await Context.Set<TEntity>()
                .AsNoTracking()
                .Where(e => !e.IsDeleted)
                .WithSpecification(spec)
                .ProjectToType<TDto>()
                .ToListAsync(cancellationToken);

            _logger.LogDebug("Retrieved {Count} {EntityType} entities using specification", result.Count, typeof(TEntity).Name);
            return result;
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("GetAsync with specification cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting {EntityType} using specification", typeof(TEntity).Name);
            throw;
        }
    }

    public virtual async Task<(List<TDto> Items, int TotalCount)> GetPagedAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Getting paged {EntityType} using specification", typeof(TEntity).Name);

            var query = Context.Set<TEntity>()
                .AsNoTracking()
                .Where(e => !e.IsDeleted)
                .WithSpecification(spec);

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .ProjectToType<TDto>()
                .ToListAsync(cancellationToken);

            _logger.LogDebug("Retrieved {ItemCount}/{TotalCount} {EntityType} entities using specification",
                items.Count, totalCount, typeof(TEntity).Name);

            return (items, totalCount);
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("GetPagedAsync with specification cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting paged {EntityType} using specification", typeof(TEntity).Name);
            throw;
        }
    }

    public virtual async Task<TDto?> FirstOrDefaultAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Getting first or default {EntityType} using specification", typeof(TEntity).Name);

            var result = await Context.Set<TEntity>()
                .AsNoTracking()
                .Where(e => !e.IsDeleted)
                .WithSpecification(spec)
                .ProjectToType<TDto>()
                .FirstOrDefaultAsync(cancellationToken);

            _logger.LogDebug("FirstOrDefault {EntityType} with specification completed", typeof(TEntity).Name);
            return result;
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("FirstOrDefaultAsync with specification cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting first or default {EntityType} using specification", typeof(TEntity).Name);
            throw;
        }
    }

    public virtual async Task<int> CountAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Counting {EntityType} using specification", typeof(TEntity).Name);

            var count = await Context.Set<TEntity>()
                .AsNoTracking()
                .Where(e => !e.IsDeleted)
                .WithSpecification(spec)
                .CountAsync(cancellationToken);

            _logger.LogDebug("{EntityType} count with specification: {Count}", typeof(TEntity).Name, count);
            return count;
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("CountAsync with specification cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error counting {EntityType} using specification", typeof(TEntity).Name);
            throw;
        }
    }

    #endregion

    #region Caching Operations

    public virtual async Task<TDto?> GetCachedAsync(Guid id, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
    {
        try
        {
            var cacheKey = $"{_cacheKeyPrefix}{id}";
            _logger.LogDebug("Getting cached {EntityType} ID {Id}", typeof(TEntity).Name, id);

            if (_memoryCache.TryGetValue<TDto>(cacheKey, out var cachedDto))
            {
                _logger.LogDebug("{EntityType} ID {Id} found in memory cache", typeof(TEntity).Name, id);
                return cachedDto;
            }

            if (_distributedCache != null)
            {
                var cachedBytes = await _distributedCache.GetAsync(cacheKey, cancellationToken);
                if (cachedBytes != null)
                {
                    var cachedJson = System.Text.Encoding.UTF8.GetString(cachedBytes);
                    cachedDto = JsonSerializer.Deserialize<TDto>(cachedJson);
                    _memoryCache.Set(cacheKey, cachedDto, TimeSpan.FromMinutes(5));
                    _logger.LogDebug("{EntityType} ID {Id} found in distributed cache", typeof(TEntity).Name, id);
                    return cachedDto;
                }
            }

            var dto = await GetByIdAsync(id, cancellationToken);
            if (dto != null)
            {
                var expirationTime = expiration ?? _defaultCacheExpiration;
                _memoryCache.Set(cacheKey, dto, expirationTime);

                if (_distributedCache != null)
                {
                    var jsonBytes = JsonSerializer.SerializeToUtf8Bytes(dto);
                    await _distributedCache.SetAsync(
                        cacheKey,
                        jsonBytes,
                        new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expirationTime },
                        cancellationToken);
                }

                _logger.LogDebug("{EntityType} ID {Id} fetched and cached", typeof(TEntity).Name, id);
            }

            return dto;
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("GetCachedAsync cancelled for {EntityType} ID {Id}", typeof(TEntity).Name, id);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting cached {EntityType} ID {Id}", typeof(TEntity).Name, id);
            throw;
        }
    }

    public virtual async Task InvalidateCacheAsync(Guid id)
    {
        try
        {
            var cacheKey = $"{_cacheKeyPrefix}{id}";
            _logger.LogDebug("Invalidating cache for {EntityType} ID {Id}", typeof(TEntity).Name, id);

            _memoryCache.Remove(cacheKey);

            if (_distributedCache != null)
            {
                await _distributedCache.RemoveAsync(cacheKey);
            }

            _logger.LogDebug("Cache invalidated for {EntityType} ID {Id}", typeof(TEntity).Name, id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error invalidating cache for {EntityType} ID {Id}", typeof(TEntity).Name, id);
        }
    }

    public virtual async Task InvalidateAllCacheAsync()
    {
        try
        {
            _logger.LogDebug("Invalidating all cache for {EntityType}", typeof(TEntity).Name);
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error invalidating all cache for {EntityType}", typeof(TEntity).Name);
        }
    }

    #endregion

    #region Async Streaming

    public virtual async IAsyncEnumerable<TDto> StreamAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Streaming {EntityType} entities", typeof(TEntity).Name);

        var query = Context.Set<TEntity>()
            .AsNoTracking()
            .Where(e => !e.IsDeleted);

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        await foreach (var entity in query.AsAsyncEnumerable().WithCancellation(cancellationToken))
        {
            yield return entity.Adapt<TDto>();
        }
    }

    public virtual async IAsyncEnumerable<List<TDto>> StreamBatchAsync(
        int batchSize,
        Expression<Func<TEntity, bool>>? predicate = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Streaming {EntityType} entities in batches of {BatchSize}", typeof(TEntity).Name, batchSize);

        var query = Context.Set<TEntity>()
            .AsNoTracking()
            .Where(e => !e.IsDeleted);

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        var batch = new List<TEntity>(batchSize);

        await foreach (var entity in query.AsAsyncEnumerable().WithCancellation(cancellationToken))
        {
            batch.Add(entity);

            if (batch.Count >= batchSize)
            {
                yield return batch.Adapt<List<TDto>>();
                batch = new List<TEntity>(batchSize);
            }
        }

        if (batch.Count > 0)
        {
            yield return batch.Adapt<List<TDto>>();
        }
    }

    #endregion

    #region Aggregation Operations

    public virtual async Task<decimal> SumAsync<TProperty>(
        Expression<Func<TEntity, TProperty>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Calculating sum for {EntityType}", typeof(TEntity).Name);

            var query = Context.Set<TEntity>()
                .AsNoTracking()
                .Where(e => !e.IsDeleted);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var sumExpression = Expression.Lambda<Func<TEntity, decimal>>(
                Expression.Convert(selector.Body, typeof(decimal)),
                selector.Parameters);

            var result = await query.SumAsync(sumExpression, cancellationToken);
            _logger.LogDebug("Sum calculated for {EntityType}: {Result}", typeof(TEntity).Name, result);
            return result;
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("SumAsync cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calculating sum for {EntityType}", typeof(TEntity).Name);
            throw;
        }
    }

    public virtual async Task<decimal> AverageAsync<TProperty>(
        Expression<Func<TEntity, TProperty>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Calculating average for {EntityType}", typeof(TEntity).Name);

            var query = Context.Set<TEntity>()
                .AsNoTracking()
                .Where(e => !e.IsDeleted);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var avgExpression = Expression.Lambda<Func<TEntity, decimal>>(
                Expression.Convert(selector.Body, typeof(decimal)),
                selector.Parameters);

            var result = await query.AverageAsync(avgExpression, cancellationToken);
            _logger.LogDebug("Average calculated for {EntityType}: {Result}", typeof(TEntity).Name, result);
            return result;
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("AverageAsync cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calculating average for {EntityType}", typeof(TEntity).Name);
            throw;
        }
    }

    public virtual async Task<TProperty?> MinAsync<TProperty>(
        Expression<Func<TEntity, TProperty>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Finding minimum for {EntityType}", typeof(TEntity).Name);

            var query = Context.Set<TEntity>()
                .AsNoTracking()
                .Where(e => !e.IsDeleted);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var result = await query.MinAsync(selector, cancellationToken);
            _logger.LogDebug("Minimum found for {EntityType}", typeof(TEntity).Name);
            return result;
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("MinAsync cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error finding minimum for {EntityType}", typeof(TEntity).Name);
            throw;
        }
    }

    public virtual async Task<TProperty?> MaxAsync<TProperty>(
        Expression<Func<TEntity, TProperty>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Finding maximum for {EntityType}", typeof(TEntity).Name);

            var query = Context.Set<TEntity>()
                .AsNoTracking()
                .Where(e => !e.IsDeleted);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var result = await query.MaxAsync(selector, cancellationToken);
            _logger.LogDebug("Maximum found for {EntityType}", typeof(TEntity).Name);
            return result;
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("MaxAsync cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error finding maximum for {EntityType}", typeof(TEntity).Name);
            throw;
        }
    }

    public virtual async Task<List<TGroupResult>> GroupByAsync<TGroupKey, TGroupResult>(
        Expression<Func<TEntity, TGroupKey>> keySelector,
        Expression<Func<IGrouping<TGroupKey, TEntity>, TGroupResult>> resultSelector,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Grouping {EntityType} entities", typeof(TEntity).Name);

            var query = Context.Set<TEntity>()
                .AsNoTracking()
                .Where(e => !e.IsDeleted);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var result = await query
                .GroupBy(keySelector)
                .Select(resultSelector)
                .ToListAsync(cancellationToken);

            _logger.LogDebug("Grouped {EntityType} into {GroupCount} groups", typeof(TEntity).Name, result.Count);
            return result;
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("GroupByAsync cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error grouping {EntityType} entities", typeof(TEntity).Name);
            throw;
        }
    }

    #endregion

    #region Advanced Query Operations

    public virtual async Task<List<TDto>> ExecuteSqlQueryAsync(string sql, object[] parameters, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Executing raw SQL query for {EntityType}", typeof(TEntity).Name);

            var entities = await Context.Set<TEntity>()
                .FromSqlRaw(sql, parameters)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            _logger.LogDebug("Raw SQL query returned {Count} {EntityType} entities", entities.Count, typeof(TEntity).Name);
            return entities.Adapt<List<TDto>>();
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("ExecuteSqlQueryAsync cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing raw SQL query for {EntityType}", typeof(TEntity).Name);
            throw;
        }
    }

    public virtual async Task<List<TDto>> ExecuteStoredProcedureAsync(string procedureName, object[] parameters, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Executing stored procedure {ProcedureName} for {EntityType}", procedureName, typeof(TEntity).Name);

            var sql = $"EXEC {procedureName} {string.Join(", ", parameters.Select((_, i) => $"@p{i}"))}";
            return await ExecuteSqlQueryAsync(sql, parameters, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing stored procedure {ProcedureName} for {EntityType}",
                procedureName, typeof(TEntity).Name);
            throw;
        }
    }

    public virtual IQueryable<TEntity> AsQueryable()
    {
        _logger.LogDebug("Creating queryable for {EntityType}", typeof(TEntity).Name);
        return Context.Set<TEntity>().Where(e => !e.IsDeleted);
    }

    public virtual IQueryable<TEntity> AsNoTrackingQueryable()
    {
        _logger.LogDebug("Creating no-tracking queryable for {EntityType}", typeof(TEntity).Name);
        return Context.Set<TEntity>()
            .AsNoTracking()
            .Where(e => !e.IsDeleted);
    }

    #endregion

    #region Transaction Support

    public virtual async Task<TResult> ExecuteInTransactionAsync<TResult>(
        Func<Task<TResult>> action,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Executing operation in transaction for {EntityType}", typeof(TEntity).Name);

            await using var transaction = await Context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var result = await action();
                await transaction.CommitAsync(cancellationToken);
                _logger.LogDebug("Transaction committed successfully for {EntityType}", typeof(TEntity).Name);
                return result;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogWarning("Transaction rolled back for {EntityType}", typeof(TEntity).Name);
                throw;
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("ExecuteInTransactionAsync cancelled for {EntityType}", typeof(TEntity).Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing operation in transaction for {EntityType}", typeof(TEntity).Name);
            throw;
        }
    }

    #endregion

    #region Business Logic Operations - Archive, Lock, Verify, Version

    /// <summary>
    /// ✅ Archive entity (calls SetArchivedFields automatically)
    /// </summary>
    public virtual async Task ArchiveAsync(Guid id, Guid? archivedByUserId = null, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Archiving {EntityType} ID {Id}", typeof(TEntity).Name, id);

            var entity = await Context.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id!.Equals(id) && !e.IsDeleted, cancellationToken);

            if (entity != null)
            {
                SetArchivedFields(entity, archivedByUserId);  // ✅ AUTO-CALL
                await InvalidateCacheAsync(id);
                _logger.LogDebug("{EntityType} ID {Id} archived", typeof(TEntity).Name, id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error archiving {EntityType} ID {Id}", typeof(TEntity).Name, id);
            throw;
        }
    }

    /// <summary>
    /// ✅ Archive multiple entities
    /// </summary>
    public virtual async Task ArchiveRangeAsync(IEnumerable<Guid> ids, Guid? archivedByUserId = null, CancellationToken cancellationToken = default)
    {
        try
        {
            var idList = ids.ToList();
            _logger.LogDebug("Archiving {Count} {EntityType} entities", idList.Count, typeof(TEntity).Name);

            var entities = await Context.Set<TEntity>()
                .Where(e => idList.Contains(e.Id) && !e.IsDeleted)
                .ToListAsync(cancellationToken);

            foreach (var entity in entities)
            {
                SetArchivedFields(entity, archivedByUserId);  // ✅ AUTO-CALL
            }

            foreach (var id in idList)
            {
                await InvalidateCacheAsync(id);
            }

            _logger.LogDebug("Archived {Count} {EntityType} entities", entities.Count, typeof(TEntity).Name);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error archiving range of {EntityType}", typeof(TEntity).Name);
            throw;
        }
    }

    /// <summary>
    /// ✅ Unarchive entity
    /// </summary>
    public virtual async Task UnarchiveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Unarchiving {EntityType} ID {Id}", typeof(TEntity).Name, id);

            var entity = await Context.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id!.Equals(id) && e.Metadata.IsArchived, cancellationToken);

            if (entity != null)
            {
                entity.Metadata.IsArchived = false;
                entity.Metadata.ArchivedAt = null;
                entity.IsActive = true;

                SetUpdatedAuditFields(entity);  // ✅ AUTO-CALL
                await InvalidateCacheAsync(id);
                _logger.LogDebug("{EntityType} ID {Id} unarchived", typeof(TEntity).Name, id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error unarchiving {EntityType} ID {Id}", typeof(TEntity).Name, id);
            throw;
        }
    }

    /// <summary>
    /// ✅ Lock entity (calls SetLockedFields automatically)
    /// </summary>
    public virtual async Task LockAsync(Guid id, string? lockReason = null, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Locking {EntityType} ID {Id}", typeof(TEntity).Name, id);

            var entity = await Context.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id!.Equals(id) && !e.IsDeleted, cancellationToken);

            if (entity != null)
            {
                if (entity.Metadata.IsLocked)
                {
                    throw new InvalidOperationException($"{typeof(TEntity).Name} with ID {id} is already locked by {entity.Metadata.LockedBy}");
                }

                SetLockedFields(entity, lockReason);  // ✅ AUTO-CALL
                await InvalidateCacheAsync(id);
                _logger.LogDebug("{EntityType} ID {Id} locked", typeof(TEntity).Name, id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error locking {EntityType} ID {Id}", typeof(TEntity).Name, id);
            throw;
        }
    }

    /// <summary>
    /// ✅ Unlock entity (calls UnlockEntity automatically)
    /// </summary>
    public virtual async Task UnlockAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Unlocking {EntityType} ID {Id}", typeof(TEntity).Name, id);

            var entity = await Context.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id!.Equals(id) && !e.IsDeleted, cancellationToken);

            if (entity != null)
            {
                UnlockEntity(entity);  // ✅ AUTO-CALL
                await InvalidateCacheAsync(id);
                _logger.LogDebug("{EntityType} ID {Id} unlocked", typeof(TEntity).Name, id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error unlocking {EntityType} ID {Id}", typeof(TEntity).Name, id);
            throw;
        }
    }

    /// <summary>
    /// ✅ Verify entity (calls SetVerifiedFields automatically)
    /// </summary>
    public virtual async Task VerifyAsync(Guid id, double? dataQualityScore = null, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Verifying {EntityType} ID {Id}", typeof(TEntity).Name, id);

            var entity = await Context.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id!.Equals(id) && !e.IsDeleted, cancellationToken);

            if (entity != null)
            {
                SetVerifiedFields(entity, dataQualityScore);  // ✅ AUTO-CALL
                await InvalidateCacheAsync(id);
                _logger.LogDebug("{EntityType} ID {Id} verified with score {Score}",
                    typeof(TEntity).Name, id, entity.Metadata.DataQualityScore);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error verifying {EntityType} ID {Id}", typeof(TEntity).Name, id);
            throw;
        }
    }

    /// <summary>
    /// ✅ Unverify entity
    /// </summary>
    public virtual async Task UnverifyAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Unverifying {EntityType} ID {Id}", typeof(TEntity).Name, id);

            var entity = await Context.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id!.Equals(id) && !e.IsDeleted, cancellationToken);

            if (entity != null)
            {
                entity.Metadata.IsVerified = false;
                entity.Metadata.VerifiedAt = null;
                entity.Metadata.VerifiedBy = null;
                entity.Metadata.DataQualityScore = null;

                SetUpdatedAuditFields(entity);  // ✅ AUTO-CALL
                await InvalidateCacheAsync(id);
                _logger.LogDebug("{EntityType} ID {Id} unverified", typeof(TEntity).Name, id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error unverifying {EntityType} ID {Id}", typeof(TEntity).Name, id);
            throw;
        }
    }

    /// <summary>
    /// ✅ Create new version (calls IncrementVersion automatically)
    /// </summary>
    public virtual async Task<TDto> CreateNewVersionAsync(Guid id, string? versionLabel = null, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Creating new version of {EntityType} ID {Id}", typeof(TEntity).Name, id);

            var entity = await Context.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id!.Equals(id) && !e.IsDeleted, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException($"{typeof(TEntity).Name} with ID {id} not found");
            }

            // Mark current version as not latest
            entity.Metadata.IsLatestVersion = false;

            // Create new version
            var newEntity = entity.Adapt<TEntity>();
            newEntity.Id = Guid.NewGuid();
            newEntity.Metadata.ParentId = entity.Id;

            IncrementVersion(newEntity, versionLabel);  // ✅ AUTO-CALL

            await Context.Set<TEntity>().AddAsync(newEntity, cancellationToken);

            _logger.LogDebug("Created new version {Version} of {EntityType}",
                newEntity.Version, typeof(TEntity).Name);

            return newEntity.Adapt<TDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating new version of {EntityType} ID {Id}", typeof(TEntity).Name, id);
            throw;
        }
    }

    /// <summary>
    /// ✅ Get all versions of an entity
    /// </summary>
    public virtual async Task<List<TDto>> GetVersionsAsync(Guid parentId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Getting versions for {EntityType} parent ID {ParentId}", typeof(TEntity).Name, parentId);

            var versions = await Context.Set<TEntity>()
                .AsNoTracking()
                .Where(e => e.Metadata.ParentId == parentId || e.Id == parentId)
                .OrderBy(e => e.Version)
                .ProjectToType<TDto>()
                .ToListAsync(cancellationToken);

            _logger.LogDebug("Found {Count} versions for {EntityType}", versions.Count, typeof(TEntity).Name);
            return versions;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting versions for {EntityType} parent ID {ParentId}",
                typeof(TEntity).Name, parentId);
            throw;
        }
    }

    /// <summary>
    /// ✅ Get latest version of an entity
    /// </summary>
    public virtual async Task<TDto?> GetLatestVersionAsync(Guid parentId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Getting latest version for {EntityType} parent ID {ParentId}",
                typeof(TEntity).Name, parentId);

            var latestVersion = await Context.Set<TEntity>()
                .AsNoTracking()
                .Where(e => (e.Metadata.ParentId == parentId || e.Id == parentId) && e.Metadata.IsLatestVersion)
                .ProjectToType<TDto>()
                .FirstOrDefaultAsync(cancellationToken);

            return latestVersion;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting latest version for {EntityType} parent ID {ParentId}",
                typeof(TEntity).Name, parentId);
            throw;
        }
    }

    #endregion
}