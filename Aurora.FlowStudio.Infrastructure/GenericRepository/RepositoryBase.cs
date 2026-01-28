using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Aurora.FlowStudio.Data.Context;
using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Infrastructure;
using Aurora.FlowStudio.Infrastructure.OData;
using Aurora.FlowStudio.Infrastructure.Results;
using EFCore.BulkExtensions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Aurora.FlowStudio.Infrastructure.Implementations;

/// <summary>
/// High-Performance DTO-First Generic Repository with ILogger
/// PRODUCTION-READY IMPLEMENTATION:
/// - ILogger dependency injection (4-parameter constructor)
/// - Comprehensive try-catch error handling in ALL methods
/// - NO direct SaveChangesAsync (UnitOfWork pattern)
/// - Dual-layer caching (Memory + Distributed)
/// - Structured logging at Debug/Info/Warning/Error levels
/// Optimized for: Big Data, High Traffic, Low Memory, Horizontal Scalability
/// </summary>
public class RepositoryBase<TEntity, TDto> : IRepository<TEntity, TDto>
	where TEntity : BaseEntity
	where TDto : class
{
	protected readonly IDatabaseFactory _databaseFactory;
	protected readonly IMemoryCache _memoryCache;
	protected readonly ILogger _logger;
	protected readonly IDistributedCache? _distributedCache;
	protected FlowStudioDbContext Context => _databaseFactory.Get();

	// Cache configuration
	private readonly TimeSpan _defaultCacheExpiration = TimeSpan.FromMinutes(15);
	private readonly string _cacheKeyPrefix;

	/// <summary>
	/// Constructor with ILogger injection - 4 parameters
	/// Compatible with UnitOfWork pattern
	/// </summary>
	public RepositoryBase(
		IDatabaseFactory databaseFactory,
		IMemoryCache memoryCache,
		ILogger logger,
		IDistributedCache? distributedCache = null)
	{
		_databaseFactory = databaseFactory ?? throw new ArgumentNullException(nameof(databaseFactory));
		_memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_distributedCache = distributedCache;
		_cacheKeyPrefix = $"{typeof(TEntity).Name}:";

		_logger.LogDebug("RepositoryBase<{EntityType}> instance created", typeof(TEntity).Name);
	}

	#region DTO-First Query Operations

	public virtual async Task<TDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
	{
		try
		{
			_logger.LogDebug("Getting {EntityType} by ID: {Id}", typeof(TEntity).Name, id);

			var result = await Context.Set<TEntity>()
				.AsNoTracking()
				.Where(e => e.Id!.Equals(id) && !e.IsDeleted)
				.ProjectToType<TDto>()
				.FirstOrDefaultAsync(cancellationToken);

			if (result != null)
				_logger.LogDebug("{EntityType} with ID {Id} found", typeof(TEntity).Name, id);
			else
				_logger.LogDebug("{EntityType} with ID {Id} not found", typeof(TEntity).Name, id);

			return result;
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
				.AsNoTracking()
				.Where(e => e.Id!.Equals(id) && !e.IsDeleted);

			foreach (var include in includes)
			{
				query = query.Include(include);
			}

			var result = await query
				.ProjectToType<TDto>()
				.FirstOrDefaultAsync();

			_logger.LogDebug("{EntityType} with ID {Id} and includes retrieved", typeof(TEntity).Name, id);
			return result;
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

	#region Command Operations (NO SaveChangesAsync)

	public virtual async Task<TDto> AddAsync(TDto dto, CancellationToken cancellationToken = default)
	{
		try
		{
			_logger.LogDebug("Adding new {EntityType}", typeof(TEntity).Name);

			var entity = dto.Adapt<TEntity>();
			entity.CreatedAt = DateTime.UtcNow;
			entity.IsDeleted = false;

			await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
			// ✅ NO SaveChangesAsync - UnitOfWork pattern

			_logger.LogDebug("New {EntityType} added to context", typeof(TEntity).Name);
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
			var now = DateTime.UtcNow;

			foreach (var entity in entities)
			{
				entity.CreatedAt = now;
				entity.IsDeleted = false;
			}

			await Context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
			// ✅ NO SaveChangesAsync - UnitOfWork pattern

			_logger.LogDebug("Added {Count} {EntityType} entities to context", entities.Count, typeof(TEntity).Name);
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
			entity.UpdatedAt = DateTime.UtcNow;

			Context.Set<TEntity>().Update(entity);
			// ✅ NO SaveChangesAsync - UnitOfWork pattern

			await InvalidateCacheAsync(entity.Id);

			_logger.LogDebug("{EntityType} updated in context", typeof(TEntity).Name);
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
			var now = DateTime.UtcNow;

			foreach (var entity in entities)
			{
				entity.UpdatedAt = now;
			}

			Context.Set<TEntity>().UpdateRange(entities);
			// ✅ NO SaveChangesAsync - UnitOfWork pattern

			foreach (var entity in entities)
			{
				await InvalidateCacheAsync(entity.Id);
			}

			_logger.LogDebug("Updated {Count} {EntityType} entities in context", entities.Count, typeof(TEntity).Name);
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

			entity.UpdatedAt = DateTime.UtcNow;
			// ✅ NO SaveChangesAsync - UnitOfWork pattern

			await InvalidateCacheAsync(id);

			_logger.LogDebug("{EntityType} ID {Id} patched successfully", typeof(TEntity).Name, id);
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
				// ✅ NO SaveChangesAsync - UnitOfWork pattern

				await InvalidateCacheAsync(id);
				_logger.LogDebug("{EntityType} ID {Id} deleted from context", typeof(TEntity).Name, id);
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
			// ✅ NO SaveChangesAsync - UnitOfWork pattern

			foreach (var id in idList)
			{
				await InvalidateCacheAsync(id);
			}

			_logger.LogDebug("Deleted {Count} {EntityType} entities from context", entities.Count, typeof(TEntity).Name);
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

	public virtual async Task SoftDeleteAsync(Guid id, Guid? deletedBy = null, CancellationToken cancellationToken = default)
	{
		try
		{
			_logger.LogDebug("Soft deleting {EntityType} ID {Id}", typeof(TEntity).Name, id);

			var entity = await Context.Set<TEntity>()
				.FirstOrDefaultAsync(e => e.Id!.Equals(id) && !e.IsDeleted, cancellationToken);

			if (entity != null)
			{
				entity.IsDeleted = true;
				entity.DeletedAt = DateTime.UtcNow;
				entity.DeletedByUserId = deletedBy;
				// ✅ NO SaveChangesAsync - UnitOfWork pattern

				await InvalidateCacheAsync(id);
				_logger.LogDebug("{EntityType} ID {Id} soft deleted", typeof(TEntity).Name, id);
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

	public virtual async Task SoftDeleteRangeAsync(IEnumerable<Guid> ids, Guid? deletedBy = null, CancellationToken cancellationToken = default)
	{
		try
		{
			var idList = ids.ToList();
			_logger.LogDebug("Soft deleting {Count} {EntityType} entities", idList.Count, typeof(TEntity).Name);

			var entities = await Context.Set<TEntity>()
				.Where(e => idList.Contains(e.Id) && !e.IsDeleted)
				.ToListAsync(cancellationToken);

			var now = DateTime.UtcNow;
			foreach (var entity in entities)
			{
				entity.IsDeleted = true;
				entity.DeletedAt = now;
				entity.DeletedByUserId = deletedBy;
			}
			// ✅ NO SaveChangesAsync - UnitOfWork pattern

			foreach (var id in idList)
			{
				await InvalidateCacheAsync(id);
			}

			_logger.LogDebug("Soft deleted {Count} {EntityType} entities", entities.Count, typeof(TEntity).Name);
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
				entity.DeletedBy = null;
				entity.UpdatedAt = DateTime.UtcNow;
				// ✅ NO SaveChangesAsync - UnitOfWork pattern

				await InvalidateCacheAsync(id);
				_logger.LogDebug("{EntityType} ID {Id} restored", typeof(TEntity).Name, id);
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
			var now = DateTime.UtcNow;

			foreach (var entity in entities)
			{
				entity.CreatedAt = now;
				entity.IsDeleted = false;
			}

			await Context.BulkInsertAsync(entities, cancellationToken: cancellationToken);

			_logger.LogInformation("Bulk inserted {Count} {EntityType} entities successfully", entities.Count, typeof(TEntity).Name);
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
			var now = DateTime.UtcNow;

			foreach (var entity in entities)
			{
				entity.UpdatedAt = now;
			}

			await Context.BulkUpdateAsync(entities, cancellationToken: cancellationToken);
			await InvalidateAllCacheAsync();

			_logger.LogInformation("Bulk updated {Count} {EntityType} entities successfully", entities.Count, typeof(TEntity).Name);
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

			_logger.LogInformation("Bulk deleted {Count} {EntityType} entities successfully", entities.Count, typeof(TEntity).Name);
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
			var now = DateTime.UtcNow;

			foreach (var entity in entities)
			{
				entity.UpdatedAt = now;
				if (entity.CreatedAt == default)
				{
					entity.CreatedAt = now;
				}
			}

			await Context.BulkInsertOrUpdateAsync(entities, cancellationToken: cancellationToken);
			await InvalidateAllCacheAsync();

			_logger.LogInformation("Bulk upserted {Count} {EntityType} entities successfully", entities.Count, typeof(TEntity).Name);
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

				_logger.LogDebug("{EntityType} ID {Id} fetched from database and cached", typeof(TEntity).Name, id);
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
			// Don't throw - cache invalidation failure shouldn't break operations
		}
	}

	public virtual async Task InvalidateAllCacheAsync()
	{
		try
		{
			_logger.LogDebug("Invalidating all cache for {EntityType}", typeof(TEntity).Name);
			// Implementation depends on cache provider
			await Task.CompletedTask;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error invalidating all cache for {EntityType}", typeof(TEntity).Name);
			// Don't throw - cache invalidation failure shouldn't break operations
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
}
