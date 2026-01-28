using System.Linq;
using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Infrastructure.OData;
using Aurora.FlowStudio.Infrastructure.Results;

namespace Aurora.FlowStudio.Infrastructure.OData;

/// <summary>
/// OData query handler interface
/// Processes OData query options and applies to IQueryable
/// </summary>
public interface IODataQueryHandler<TEntity> where TEntity : class
{
    /// <summary>
    /// Apply OData query options to IQueryable
    /// </summary>
    IQueryable<TEntity> ApplyQuery(IQueryable<TEntity> query, ODataQueryOptions options);

    /// <summary>
    /// Execute OData query and return paged result
    /// </summary>
    Task<ODataPagedResult<TEntity>> ExecuteQueryAsync(
        IQueryable<TEntity> query,
        ODataQueryOptions options,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Execute OData query with projection
    /// </summary>
    Task<ODataPagedResult<TResult>> ExecuteQueryAsync<TResult>(
        IQueryable<TEntity> query,
        ODataQueryOptions options,
        CancellationToken cancellationToken = default);
}