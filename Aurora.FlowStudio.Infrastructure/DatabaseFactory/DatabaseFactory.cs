using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Aurora.FlowStudio.Data.Context;

namespace Aurora.FlowStudio.Infrastructure;

/// <summary>
/// Database factory implementation
/// Provides lazy initialization and proper disposal of DbContext
/// Thread-safe singleton pattern per request scope
/// </summary>
public class DatabaseFactory : IDatabaseFactory
{
    private readonly DbContextOptions<FlowStudioDbContext> _options;
    private readonly ILogger<DatabaseFactory> _logger;
    private FlowStudioDbContext? _context;
    private bool _disposed;

    public DatabaseFactory(
        DbContextOptions<FlowStudioDbContext> options,
        ILogger<DatabaseFactory> logger)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Get or create DbContext instance (lazy initialization)
    /// </summary>
    public FlowStudioDbContext Get()
    {
        if (_context == null)
        {
            _logger.LogDebug("Creating new FlowStudioDbContext instance");
            _context = new FlowStudioDbContext(_options);
        }

        return _context;
    }

    /// <summary>
    /// Get or create DbContext instance (async - for consistency)
    /// </summary>
    public Task<FlowStudioDbContext> GetAsync()
    {
        return Task.FromResult(Get());
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _logger.LogDebug("Disposing FlowStudioDbContext instance");
                    _context.Dispose();
                    _context = null;
                }
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