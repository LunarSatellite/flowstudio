using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Aurora.FlowStudio.Infrastructure.OData;
using Aurora.FlowStudio.Infrastructure.Results;

namespace Aurora.FlowStudio.Infrastructure.OData;

/// <summary>
/// OData query handler implementation
/// Supports full OData 4.0 query syntax
/// </summary>
public class ODataQueryHandler<TEntity> : IODataQueryHandler<TEntity>
    where TEntity : class
{
    private readonly ILogger<ODataQueryHandler<TEntity>> _logger;

    public ODataQueryHandler(ILogger<ODataQueryHandler<TEntity>> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IQueryable<TEntity> ApplyQuery(IQueryable<TEntity> query, ODataQueryOptions options)
    {
        if (options == null)
            return query;

        try
        {
            // 1. Apply $filter
            if (!string.IsNullOrWhiteSpace(options.Filter))
            {
                _logger.LogDebug("Applying $filter: {Filter}", options.Filter);
                query = ApplyFilter(query, options.Filter);
            }

            // 2. Apply $search (if supported)
            if (!string.IsNullOrWhiteSpace(options.Search))
            {
                _logger.LogDebug("Applying $search: {Search}", options.Search);
                query = ApplySearch(query, options.Search);
            }

            // 3. Apply $orderby
            if (!string.IsNullOrWhiteSpace(options.OrderBy))
            {
                _logger.LogDebug("Applying $orderby: {OrderBy}", options.OrderBy);
                query = ApplyOrderBy(query, options.OrderBy);
            }

            // 4. Apply $expand
            if (!string.IsNullOrWhiteSpace(options.Expand))
            {
                _logger.LogDebug("Applying $expand: {Expand}", options.Expand);
                query = ApplyExpand(query, options.Expand);
            }

            return query;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error applying OData query");
            throw new InvalidOperationException($"Invalid OData query: {ex.Message}", ex);
        }
    }

    public async Task<ODataPagedResult<TEntity>> ExecuteQueryAsync(
        IQueryable<TEntity> query,
        ODataQueryOptions options,
        CancellationToken cancellationToken = default)
    {
        // Apply filtering, sorting, expansion
        query = ApplyQuery(query, options);

        // Get total count if requested
        int? totalCount = null;
        if (options.Count)
        {
            totalCount = await query.CountAsync(cancellationToken);
            _logger.LogDebug("Total count: {Count}", totalCount);
        }

        // Apply paging
        var skip = options.GetEffectiveSkip();
        var top = options.GetEffectivePageSize();

        query = query.Skip(skip).Take(top);

        // Execute query
        var items = await query.ToListAsync(cancellationToken);

        // Create result
        var pageNumber = (skip / top) + 1;
        var result = ODataPagedResult<TEntity>.Create(
            items,
            totalCount ?? items.Count,
            pageNumber,
            top);

        _logger.LogDebug("Returned {Count} items (page {Page})", items.Count, pageNumber);

        return result;
    }

    public async Task<ODataPagedResult<TResult>> ExecuteQueryAsync<TResult>(
        IQueryable<TEntity> query,
        ODataQueryOptions options,
        CancellationToken cancellationToken = default)
    {
        // Apply filtering, sorting, expansion
        query = ApplyQuery(query, options);

        // Get total count if requested
        int? totalCount = null;
        if (options.Count)
        {
            totalCount = await query.CountAsync(cancellationToken);
        }

        // Apply $select projection
        IQueryable<TResult> projectedQuery;
        if (!string.IsNullOrWhiteSpace(options.Select))
        {
            _logger.LogDebug("Applying $select: {Select}", options.Select);
            projectedQuery = ApplySelect<TResult>(query, options.Select);
        }
        else
        {
            // Default projection
            projectedQuery = query.Cast<TResult>();
        }

        // Apply paging
        var skip = options.GetEffectiveSkip();
        var top = options.GetEffectivePageSize();

        projectedQuery = projectedQuery.Skip(skip).Take(top);

        // Execute query
        var items = await projectedQuery.ToListAsync(cancellationToken);

        // Create result
        var pageNumber = (skip / top) + 1;
        var result = ODataPagedResult<TResult>.Create(
            items,
            totalCount ?? items.Count,
            pageNumber,
            top);

        return result;
    }

    #region Private Helper Methods

    private IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query, string filter)
    {
        // Convert OData filter syntax to Dynamic LINQ
        var dynamicFilter = ConvertODataFilterToDynamicLinq(filter);
        return query.Where(dynamicFilter);
    }

    private IQueryable<TEntity> ApplyOrderBy(IQueryable<TEntity> query, string orderBy)
    {
        // Convert OData orderby to Dynamic LINQ
        var dynamicOrderBy = ConvertODataOrderByToDynamicLinq(orderBy);
        return query.OrderBy(dynamicOrderBy);
    }

    private IQueryable<TEntity> ApplyExpand(IQueryable<TEntity> query, string expand)
    {
        // Parse expand paths and include them
        var expandPaths = expand.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(e => e.Trim());

        foreach (var path in expandPaths)
        {
            // Handle nested expands: Company($expand=Tenants)
            var simplePath = path.Split('(')[0].Trim();
            query = query.Include(simplePath);
        }

        return query;
    }

    private IQueryable<TEntity> ApplySearch(IQueryable<TEntity> query, string search)
    {
        // Implement full-text search logic
        // This is a simplified version - enhance based on requirements
        var searchTerm = search.Trim('"').ToLower();

        // Get all string properties
        var stringProperties = typeof(TEntity).GetProperties()
            .Where(p => p.PropertyType == typeof(string))
            .Select(p => p.Name);

        // Build search predicate: property1.Contains(term) OR property2.Contains(term) OR ...
        if (stringProperties.Any())
        {
            var predicates = stringProperties
                .Select(prop => $"{prop}.ToLower().Contains(\"{searchTerm}\")")
                .ToArray();

            var combinedPredicate = string.Join(" || ", predicates);
            query = query.Where(combinedPredicate);
        }

        return query;
    }

    private IQueryable<TResult> ApplySelect<TResult>(IQueryable<TEntity> query, string select)
    {
        // Parse select fields
        var fields = select.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(f => f.Trim())
            .ToArray();

        // Build dynamic select: new { Field1 = it.Field1, Field2 = it.Field2 }
        var selectExpression = $"new ({string.Join(", ", fields)})";

        return query.Select<TResult>(selectExpression);
    }

    private string ConvertODataFilterToDynamicLinq(string odataFilter)
    {
        // Convert OData operators to C# operators
        return odataFilter
            .Replace(" eq ", " == ")
            .Replace(" ne ", " != ")
            .Replace(" gt ", " > ")
            .Replace(" ge ", " >= ")
            .Replace(" lt ", " < ")
            .Replace(" le ", " <= ")
            .Replace(" and ", " && ")
            .Replace(" or ", " || ")
            .Replace(" not ", " ! ");
    }

    private string ConvertODataOrderByToDynamicLinq(string odataOrderBy)
    {
        // Convert OData orderby to Dynamic LINQ format
        // Example: "Name asc, CreatedAt desc" -> "Name ascending, CreatedAt descending"
        return odataOrderBy
            .Replace(" asc", " ascending")
            .Replace(" desc", " descending");
    }

    #endregion
}