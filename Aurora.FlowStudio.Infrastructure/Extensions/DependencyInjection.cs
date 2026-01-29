using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.FlowStudio.Infrastructure.Extensions;

/// <summary>
/// Infrastructure layer dependency injection configuration
/// FIXED VERSION - includes caching services registration
/// Registers DatabaseFactory, UnitOfWork, caching, and base repository patterns
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Register all infrastructure services
    /// Call this method from Program.cs: AddInfrastructureServices
    /// </summary>
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // ✅ FIXED: Register caching services (REQUIRED by RepositoryBase)
        services.AddMemoryCache();
        services.AddDistributedMemoryCache(); // Default in-memory distributed cache
        // For Redis: services.AddStackExchangeRedisCache(options => { ... });

        // Register DatabaseFactory - Creates and manages DbContext instances
        services.AddScoped<IDatabaseFactory, DatabaseFactory>();

        // Register UnitOfWork - Transaction management and SaveChanges coordination
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Register Generic Repository - Base repository for all entities
        // Supports: IRepository<TEntity, TDto>
        services.AddScoped(typeof(IRepository<,>), typeof(Implementations.RepositoryBase<,>));

        // Register OData Query Handler - Advanced filtering, sorting, paging
        services.AddScoped(typeof(OData.IODataQueryHandler<>), typeof(OData.ODataQueryHandler<>));

        return services;
    }

    /// <summary>
    /// Legacy method name for backward compatibility
    /// </summary>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return AddInfrastructureServices(services, configuration);
    }

    /// <summary>
    /// Configure Redis distributed cache (optional)
    /// Call this after AddInfrastructureServices if you want to use Redis
    /// </summary>
    public static IServiceCollection AddRedisCache(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var redisConnection = configuration.GetConnectionString("Redis");
        if (!string.IsNullOrEmpty(redisConnection))
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnection;
                options.InstanceName = "FlowStudio:";
            });
        }

        return services;
    }
}
