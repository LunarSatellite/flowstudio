using Microsoft.EntityFrameworkCore;
using Aurora.FlowStudio.Data.Context;

namespace Aurora.FlowStudio.Infrastructure;

/// <summary>
/// Database factory interface for managing DbContext lifecycle
/// Implements lazy initialization and proper disposal patterns
/// </summary>
public interface IDatabaseFactory : IDisposable
{
    /// <summary>
    /// Get or create DbContext instance
    /// </summary>
    FlowStudioDbContext Get();

    /// <summary>
    /// Get or create DbContext instance (async)
    /// </summary>
    Task<FlowStudioDbContext> GetAsync();
}