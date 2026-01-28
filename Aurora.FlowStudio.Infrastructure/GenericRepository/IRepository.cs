using System.Linq.Expressions;
using Ardalis.Specification;
using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Infrastructure.OData;
using Aurora.FlowStudio.Infrastructure.Results;

namespace Aurora.FlowStudio.Infrastructure;

/// <summary>
/// DTO-FIRST Generic Repository Interface
/// Optimized for high-traffic, big data scenarios
/// ALL operations work with DTOs to minimize memory footprint and maximize performance
/// </summary>
/// <typeparam name="TEntity">Domain entity type</typeparam>
/// <typeparam name="TDto">Data Transfer Object type</typeparam>
/// <typeparam name="Guid">Primary key type</typeparam>
public interface IRepository<TEntity, TDto>
    where TEntity : BaseEntity
    where TDto : class
{
    #region DTO-First Query Operations (High Performance)

    /// <summary>
    /// Get single DTO by ID (cached, optimized projection)
    /// </summary>
    Task<TDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get single DTO by ID with custom includes
    /// </summary>
    Task<TDto?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes);

    /// <summary>
    /// Get all DTOs with optional filtering (use with caution on large datasets)
    /// </summary>
    Task<List<TDto>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Find DTOs matching predicate (optimized projection)
    /// </summary>
    Task<List<TDto>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get first or default DTO matching predicate
    /// </summary>
    Task<TDto?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Check existence without loading data
    /// </summary>
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Count records matching predicate
    /// </summary>
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Count records (long for big data scenarios)
    /// </summary>
    Task<long> LongCountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);

    #endregion

    #region OData Query Operations (For Big Data & High Traffic)

    /// <summary>
    /// Execute OData query returning DTOs (server-side filtering, paging, sorting)
    /// Optimized for large datasets with minimal memory usage
    /// </summary>
    Task<ODataPagedResult<TDto>> QueryAsync(
        ODataQueryOptions options,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Execute OData query with base filter
    /// </summary>
    Task<ODataPagedResult<TDto>> QueryAsync(
        Expression<Func<TEntity, bool>> baseFilter,
        ODataQueryOptions options,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Execute OData query with custom projection
    /// </summary>
    Task<ODataPagedResult<TResult>> QueryAsync<TResult>(
        ODataQueryOptions options,
        CancellationToken cancellationToken = default) where TResult : class;

    #endregion

    #region Paging Operations (Optimized for High Traffic)

    /// <summary>
    /// Get paged DTOs with efficient counting
    /// </summary>
    Task<(List<TDto> Items, int TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get paged DTOs with cursor-based pagination (for very large datasets)
    /// </summary>
    //Task<(List<TDto> Items? NextCursor)> GetPagedByCursorAsync(
    //    Guid? cursor,
    //    int pageSize,
    //    Expression<Func<TEntity, bool>>? filter = null,
    //    CancellationToken cancellationToken = default);

    #endregion

    #region Command Operations (DTO-First)

    /// <summary>
    /// Add entity from DTO, returns created DTO
    /// </summary>
    Task<TDto> AddAsync(TDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Add multiple entities from DTOs (batched for performance)
    /// </summary>
    Task<List<TDto>> AddRangeAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update entity from DTO
    /// </summary>
    Task<TDto> UpdateAsync(TDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update multiple entities from DTOs (batched)
    /// </summary>
    Task UpdateRangeAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default);

    /// <summary>
    /// Partial update (only specified properties)
    /// </summary>
    Task<TDto> PatchAsync(Guid id, Dictionary<string, object> updates, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete by ID
    /// </summary>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete multiple by IDs (batched)
    /// </summary>
    Task DeleteRangeAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    #endregion

    #region Soft Delete Operations

    /// <summary>
    /// Soft delete by ID
    /// </summary>
    Task SoftDeleteAsync(Guid id, Guid? deletedBy = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Soft delete multiple by IDs
    /// </summary>
    Task SoftDeleteRangeAsync(IEnumerable<Guid> ids, Guid? deletedBy = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Restore soft-deleted entity
    /// </summary>
    Task RestoreAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get soft-deleted DTOs
    /// </summary>
    Task<List<TDto>> GetDeletedAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);

    #endregion

    #region Bulk Operations (For Big Data & High Traffic)

    /// <summary>
    /// Bulk insert (optimized for large datasets, bypasses change tracking)
    /// Can handle millions of records efficiently
    /// </summary>
    Task<int> BulkInsertAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default);

    /// <summary>
    /// Bulk update (optimized for large datasets)
    /// </summary>
    Task<int> BulkUpdateAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default);

    /// <summary>
    /// Bulk delete by predicate (optimized for large datasets)
    /// </summary>
    Task<int> BulkDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Bulk upsert (insert or update based on key)
    /// </summary>
    Task<int> BulkUpsertAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default);

    #endregion

    #region Specification Pattern (For Complex Queries)

    /// <summary>
    /// Get DTOs using specification pattern
    /// </summary>
    Task<List<TDto>> GetAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get paged DTOs using specification
    /// </summary>
    Task<(List<TDto> Items, int TotalCount)> GetPagedAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get first or default using specification
    /// </summary>
    Task<TDto?> FirstOrDefaultAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);

    /// <summary>
    /// Count using specification
    /// </summary>
    Task<int> CountAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);

    #endregion

    #region Caching Operations (For High-Traffic Scenarios)

    /// <summary>
    /// Get cached DTO by ID (Redis/Memory cache)
    /// </summary>
    Task<TDto?> GetCachedAsync(Guid id, TimeSpan? expiration = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Invalidate cache for specific ID
    /// </summary>
    Task InvalidateCacheAsync(Guid id);

    /// <summary>
    /// Invalidate all cache for this entity type
    /// </summary>
    Task InvalidateAllCacheAsync();

    #endregion

    #region Async Streaming (For Very Large Datasets)

    /// <summary>
    /// Stream DTOs for processing large datasets without loading all into memory
    /// Perfect for export, ETL, analytics on millions of records
    /// </summary>
    IAsyncEnumerable<TDto> StreamAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Stream in batches for bulk processing
    /// </summary>
    IAsyncEnumerable<List<TDto>> StreamBatchAsync(
        int batchSize,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    #endregion

    #region Aggregation Operations (For Analytics on Big Data)

    /// <summary>
    /// Sum numeric field
    /// </summary>
    Task<decimal> SumAsync<TProperty>(
        Expression<Func<TEntity, TProperty>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Average numeric field
    /// </summary>
    Task<decimal> AverageAsync<TProperty>(
        Expression<Func<TEntity, TProperty>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Min value
    /// </summary>
    Task<TProperty?> MinAsync<TProperty>(
        Expression<Func<TEntity, TProperty>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Max value
    /// </summary>
    Task<TProperty?> MaxAsync<TProperty>(
        Expression<Func<TEntity, TProperty>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Group by and aggregate
    /// </summary>
    Task<List<TGroupResult>> GroupByAsync<TGroupKey, TGroupResult>(
        Expression<Func<TEntity, TGroupKey>> keySelector,
        Expression<Func<IGrouping<TGroupKey, TEntity>, TGroupResult>> resultSelector,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    #endregion

    #region Advanced Query Operations

    /// <summary>
    /// Execute raw SQL query returning DTOs (for complex queries)
    /// </summary>
    Task<List<TDto>> ExecuteSqlQueryAsync(string sql, object[] parameters, CancellationToken cancellationToken = default);

    /// <summary>
    /// Execute stored procedure returning DTOs
    /// </summary>
    Task<List<TDto>> ExecuteStoredProcedureAsync(string procedureName, object[] parameters, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get queryable for custom LINQ operations (use carefully)
    /// </summary>
    IQueryable<TEntity> AsQueryable();

    /// <summary>
    /// Get no-tracking queryable for read-only operations (better performance)
    /// </summary>
    IQueryable<TEntity> AsNoTrackingQueryable();

    #endregion

    #region Transaction Support

    /// <summary>
    /// Execute action within transaction
    /// </summary>
    Task<TResult> ExecuteInTransactionAsync<TResult>(
        Func<Task<TResult>> action,
        CancellationToken cancellationToken = default);

    #endregion
}