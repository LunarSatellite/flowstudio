using System.Collections.Generic;

namespace Aurora.FlowStudio.Infrastructure.OData;

/// <summary>
/// OData query options for dynamic querying
/// Supports OData 4.0 protocol specification
/// </summary>
public class ODataQueryOptions
{
    /// <summary>
    /// $filter - Filter results by conditions
    /// Example: $filter=Name eq 'John' and Age gt 25
    /// </summary>
    public string? Filter { get; set; }

    /// <summary>
    /// $orderby - Order results
    /// Example: $orderby=Name asc, CreatedAt desc
    /// </summary>
    public string? OrderBy { get; set; }

    /// <summary>
    /// $select - Select specific fields
    /// Example: $select=Id,Name,Email
    /// </summary>
    public string? Select { get; set; }

    /// <summary>
    /// $expand - Expand navigation properties
    /// Example: $expand=UserProfile,Company($expand=Tenants)
    /// </summary>
    public string? Expand { get; set; }

    /// <summary>
    /// $top - Limit number of results
    /// Example: $top=10
    /// </summary>
    public int? Top { get; set; }

    /// <summary>
    /// $skip - Skip number of results
    /// Example: $skip=20
    /// </summary>
    public int? Skip { get; set; }

    /// <summary>
    /// $count - Include total count
    /// Example: $count=true
    /// </summary>
    public bool Count { get; set; } = false;

    /// <summary>
    /// $search - Full-text search
    /// Example: $search="john smith"
    /// </summary>
    public string? Search { get; set; }

    /// <summary>
    /// $apply - Aggregation and grouping
    /// Example: $apply=groupby((Category),aggregate(Amount with sum as Total))
    /// </summary>
    public string? Apply { get; set; }

    /// <summary>
    /// Maximum page size (server-side limit)
    /// </summary>
    public int MaxPageSize { get; set; } = 100;

    /// <summary>
    /// Default page size when $top is not specified
    /// </summary>
    public int DefaultPageSize { get; set; } = 20;

    /// <summary>
    /// Calculate effective page size
    /// </summary>
    public int GetEffectivePageSize()
    {
        if (Top.HasValue)
        {
            return Math.Min(Top.Value, MaxPageSize);
        }
        return DefaultPageSize;
    }

    /// <summary>
    /// Calculate effective skip
    /// </summary>
    public int GetEffectiveSkip()
    {
        return Skip ?? 0;
    }

    /// <summary>
    /// Convert to query string
    /// </summary>
    public string ToQueryString()
    {
        var parameters = new List<string>();

        if (!string.IsNullOrWhiteSpace(Filter))
            parameters.Add($"$filter={Uri.EscapeDataString(Filter)}");

        if (!string.IsNullOrWhiteSpace(OrderBy))
            parameters.Add($"$orderby={Uri.EscapeDataString(OrderBy)}");

        if (!string.IsNullOrWhiteSpace(Select))
            parameters.Add($"$select={Uri.EscapeDataString(Select)}");

        if (!string.IsNullOrWhiteSpace(Expand))
            parameters.Add($"$expand={Uri.EscapeDataString(Expand)}");

        if (Top.HasValue)
            parameters.Add($"$top={Top.Value}");

        if (Skip.HasValue)
            parameters.Add($"$skip={Skip.Value}");

        if (Count)
            parameters.Add("$count=true");

        if (!string.IsNullOrWhiteSpace(Search))
            parameters.Add($"$search={Uri.EscapeDataString(Search)}");

        if (!string.IsNullOrWhiteSpace(Apply))
            parameters.Add($"$apply={Uri.EscapeDataString(Apply)}");

        return string.Join("&", parameters);
    }
}