using Aurora.FlowStudio.Infrastructure.OData;
using Aurora.FlowStudio.Infrastructure;
using Aurora.FlowStudio.Infrastructure.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.FlowStudio.Infrastructure.Extensions;

/// <summary>
/// Infrastructure layer dependency injection configuration
/// Registers DatabaseFactory, UnitOfWork, and base repository patterns
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Register all infrastructure services
    /// Call this method name from Program.cs: AddInfrastructureServices
    /// </summary>
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register DatabaseFactory - Creates and manages DbContext instances
        services.AddScoped<IDatabaseFactory, DatabaseFactory>();

        // Register UnitOfWork - Transaction management and SaveChanges coordination
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Register Generic Repository - Base repository for all entities
        // Supports: IRepository<TEntity, TDto>
        services.AddScoped(typeof(IRepository<,>), typeof(RepositoryBase<,>));

        // Register OData Query Handler - Advanced filtering, sorting, paging
        services.AddScoped(typeof(IODataQueryHandler<>), typeof(ODataQueryHandler<>));

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
}