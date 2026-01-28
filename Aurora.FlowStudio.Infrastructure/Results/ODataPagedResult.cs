using System.Collections.Generic;

namespace Aurora.FlowStudio.Infrastructure.Results;

/// <summary>
/// OData-compliant paged result
/// Follows OData 4.0 JSON format specification
/// </summary>
public class ODataPagedResult<T>
{
    /// <summary>
    /// OData context (metadata)
    /// </summary>
    public string? Context { get; set; }

    /// <summary>
    /// Total count of items (when $count=true)
    /// </summary>
    public int? Count { get; set; }

    /// <summary>
    /// Array of items
    /// </summary>
    public List<T> Value { get; set; } = new();

    /// <summary>
    /// Next link for pagination
    /// </summary>
    public string? NextLink { get; set; }

    /// <summary>
    /// Previous link for pagination
    /// </summary>
    public string? PrevLink { get; set; }

    /// <summary>
    /// Delta link for change tracking
    /// </summary>
    public string? DeltaLink { get; set; }

    /// <summary>
    /// Current page number (not in OData spec, but useful)
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Page size
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Total pages
    /// </summary>
    public int TotalPages => Count.HasValue && PageSize > 0
        ? (int)Math.Ceiling(Count.Value / (double)PageSize)
        : 0;

    /// <summary>
    /// Has previous page
    /// </summary>
    public bool HasPreviousPage => CurrentPage > 1;

    /// <summary>
    /// Has next page
    /// </summary>
    public bool HasNextPage => CurrentPage < TotalPages;

    /// <summary>
    /// Create OData result from paged data
    /// </summary>
    public static ODataPagedResult<T> Create(
        List<T> items,
        int totalCount,
        int pageNumber,
        int pageSize,
        string? baseUrl = null,
        string? queryString = null)
    {
        var result = new ODataPagedResult<T>
        {
            Value = items,
            Count = totalCount,
            CurrentPage = pageNumber,
            PageSize = pageSize
        };

        // Generate next/prev links if baseUrl provided
        if (!string.IsNullOrWhiteSpace(baseUrl))
        {
            var query = !string.IsNullOrWhiteSpace(queryString) ? queryString : "";

            if (result.HasNextPage)
            {
                result.NextLink = $"{baseUrl}?$skip={pageNumber * pageSize}&$top={pageSize}&{query}";
            }

            if (result.HasPreviousPage)
            {
                result.PrevLink = $"{baseUrl}?$skip={(pageNumber - 2) * pageSize}&$top={pageSize}&{query}";
            }
        }

        return result;
    }
}