namespace Aurora.FlowStudio.Infrastructure.Services;

/// <summary>
/// Extended service to get current authenticated user information
/// Used for comprehensive audit tracking in BaseEntity
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// Get current user's ID (from JWT token or authentication context)
    /// </summary>
    Guid? GetUserId();

    /// <summary>
    /// Get current user's username/email
    /// </summary>
    string? GetUserName();

    /// <summary>
    /// Get current user's username/email
    /// </summary>
    string? GetUserEmail();

    /// <summary>
    /// Get current user's tenant ID (for multi-tenancy)
    /// </summary>
    Guid? GetTenantId();

    /// <summary>
    /// Check if user is authenticated
    /// </summary>
    bool IsAuthenticated();

    /// <summary>
    /// Get user's IP address
    /// </summary>
    string? GetIpAddress();

    /// <summary>
    /// Get user's User-Agent (browser/device info)
    /// </summary>
    string? GetUserAgent();

    /// <summary>
    /// Get user's timezone
    /// </summary>
    string? GetTimezone();

    /// <summary>
    /// Get user's country/region
    /// </summary>
    string? GetCountry();

    /// <summary>
    /// Get user's preferred language
    /// </summary>
    string? GetLanguage();
}