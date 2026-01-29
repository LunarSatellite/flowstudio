using Aurora.FlowStudio.Entity.Base;
using Aurora.FlowStudio.Infrastructure.Implementations;
using Aurora.FlowStudio.Infrastructure.Services;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Aurora.FlowStudio.Infrastructure;

/// <summary>
/// Unit of Work implementation using generic repository pattern
/// FIXED VERSION - includes IMemoryCache, IDistributedCache, and ICurrentUserService
/// Repositories created on-demand and cached
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly IDatabaseFactory _databaseFactory;
    private readonly ILogger<UnitOfWork> _logger;
    private readonly ILoggerFactory _loggerFactory;
    private readonly IMemoryCache _memoryCache;
    private readonly IDistributedCache? _distributedCache;
    private readonly ICurrentUserService? _currentUserService;
    private readonly Dictionary<Type, object> _repositories;
    private IDbContextTransaction? _currentTransaction;
    private bool _disposed;

    public UnitOfWork(
        IDatabaseFactory databaseFactory,
        ILogger<UnitOfWork> logger,
        ILoggerFactory loggerFactory,
        IMemoryCache memoryCache,
        IDistributedCache? distributedCache = null,
        ICurrentUserService? currentUserService = null)
    {
        _databaseFactory = databaseFactory ?? throw new ArgumentNullException(nameof(databaseFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        _distributedCache = distributedCache;
        _currentUserService = currentUserService;
        _repositories = new Dictionary<Type, object>();
    }

    /// <summary>
    /// Get repository for any entity type
    /// Creates and caches repository instance on first access
    /// FIXED: Now passes all 5 required parameters to RepositoryBase constructor
    /// </summary>
    public IRepository<TEntity, TDto> Repository<TEntity, TDto>()
        where TEntity : BaseEntity
        where TDto : class
    {
        var type = typeof(TEntity);

        if (!_repositories.ContainsKey(type))
        {
            _logger.LogDebug("Creating repository for entity type: {EntityType}", type.Name);

            var repositoryType = typeof(RepositoryBase<,>).MakeGenericType(typeof(TEntity), typeof(TDto));
            var repositoryLogger = _loggerFactory.CreateLogger(repositoryType);

            // ✅ FIXED: Pass all 5 required parameters (added ICurrentUserService)
            var repositoryInstance = Activator.CreateInstance(
                repositoryType,
                _databaseFactory,
                _memoryCache,
                repositoryLogger,
                _distributedCache,
                _currentUserService) ?? throw new InvalidOperationException($"Could not create repository for {type.Name}");

            _repositories.Add(type, repositoryInstance);
        }

        return (IRepository<TEntity, TDto>)_repositories[type];
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var context = _databaseFactory.Get();
            var result = await context.SaveChangesAsync(cancellationToken);
            _logger.LogDebug("Saved {Count} changes to database", result);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving changes to database");
            throw;
        }
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction != null)
        {
            _logger.LogWarning("Transaction already in progress");
            return _currentTransaction;
        }

        var context = _databaseFactory.Get();
        _currentTransaction = await context.Database.BeginTransactionAsync(cancellationToken);
        _logger.LogDebug("Transaction started");
        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction == null)
        {
            _logger.LogWarning("No transaction to commit");
            return;
        }

        try
        {
            await SaveChangesAsync(cancellationToken);
            await _currentTransaction.CommitAsync(cancellationToken);
            _logger.LogDebug("Transaction committed");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error committing transaction");
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction == null)
        {
            _logger.LogWarning("No transaction to rollback");
            return;
        }

        try
        {
            await _currentTransaction.RollbackAsync(cancellationToken);
            _logger.LogDebug("Transaction rolled back");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error rolling back transaction");
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }

    public async Task<T> ExecuteInTransactionAsync<T>(
        Func<Task<T>> operation,
        CancellationToken cancellationToken = default)
    {
        await using var transaction = await BeginTransactionAsync(cancellationToken);

        try
        {
            var result = await operation();
            await CommitTransactionAsync(cancellationToken);
            return result;
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _currentTransaction?.Dispose();
                _databaseFactory?.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}