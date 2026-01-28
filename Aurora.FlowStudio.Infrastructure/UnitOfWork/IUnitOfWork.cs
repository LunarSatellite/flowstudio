using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aurora.FlowStudio.Infrastructure;

/// <summary>
/// Unit of Work interface using generic repository pattern
/// Follows exact pattern from uploaded UnitOfWork.cs
/// Access any repository via Repository<TEntity>() method
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Get repository for any entity type
    /// Creates repository on-demand if not already cached
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="Guid">Primary key type</typeparam>
    /// <returns>Repository instance for the entity</returns>
    IRepository<TEntity, TDto> Repository<TEntity, TDto>()
        where TEntity : BaseEntity
        where TDto : class;

    /// <summary>
    /// Save all changes to database
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Begin a new transaction
    /// </summary>
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Commit current transaction
    /// </summary>
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Rollback current transaction
    /// </summary>
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Execute operation within transaction
    /// Automatically commits on success, rolls back on exception
    /// </summary>
    Task<T> ExecuteInTransactionAsync<T>(
        Func<Task<T>> operation,
        CancellationToken cancellationToken = default);
}