using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Aurora.FlowStudio.Infrastructure.Services;

/// <summary>
/// Extended implementation of ICurrentUserService using HttpContext
/// Extracts comprehensive user information from JWT claims and HTTP context
/// </summary>
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public Guid? GetUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value
            ?? _httpContextAccessor.HttpContext?.User?.FindFirst("userId")?.Value;

        if (!string.IsNullOrEmpty(userIdClaim) && Guid.TryParse(userIdClaim, out var userId))
        {
            return userId;
        }

        return null;
    }

    public string? GetUserName()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value
            ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value
            ?? _httpContextAccessor.HttpContext?.User?.FindFirst("username")?.Value;
    }

    public Guid? GetTenantId()
    {
        var tenantIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("tenantId")?.Value
            ?? _httpContextAccessor.HttpContext?.User?.FindFirst("tenant_id")?.Value;

        if (!string.IsNullOrEmpty(tenantIdClaim) && Guid.TryParse(tenantIdClaim, out var tenantId))
        {
            return tenantId;
        }

        return null;
    }

    public bool IsAuthenticated()
    {
        return _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }

    public string? GetIpAddress()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null) return null;

        // Try X-Forwarded-For header first (for proxies/load balancers)
        var forwardedFor = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrEmpty(forwardedFor))
        {
            var ips = forwardedFor.Split(',');
            return ips[0].Trim(); // First IP is the original client
        }

        // Try X-Real-IP header
        var realIp = httpContext.Request.Headers["X-Real-IP"].FirstOrDefault();
        if (!string.IsNullOrEmpty(realIp))
        {
            return realIp;
        }

        // Fall back to RemoteIpAddress
        return httpContext.Connection.RemoteIpAddress?.ToString();
    }

    public string? GetUserAgent()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null) return null;

        var userAgent = httpContext.Request.Headers["User-Agent"].FirstOrDefault();

        // Truncate to 500 chars to match BaseEntity constraint
        if (!string.IsNullOrEmpty(userAgent) && userAgent.Length > 500)
        {
            return userAgent.Substring(0, 500);
        }

        return userAgent;
    }

    public string? GetTimezone()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst("timezone")?.Value
            ?? _httpContextAccessor.HttpContext?.User?.FindFirst("tz")?.Value;
    }

    public string? GetCountry()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst("country")?.Value
            ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Country)?.Value;
    }

    public string? GetLanguage()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null) return null;

        // Try claim first
        var langClaim = httpContext.User?.FindFirst("language")?.Value
            ?? httpContext.User?.FindFirst("lang")?.Value;

        if (!string.IsNullOrEmpty(langClaim))
        {
            return langClaim;
        }

        // Fall back to Accept-Language header
        var acceptLanguage = httpContext.Request.Headers["Accept-Language"].FirstOrDefault();
        if (!string.IsNullOrEmpty(acceptLanguage))
        {
            // Parse "en-US,en;q=0.9" -> "en-US"
            var primaryLang = acceptLanguage.Split(',')[0].Split(';')[0].Trim();
            return primaryLang;
        }

        return null;
    }
}