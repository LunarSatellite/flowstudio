using Aurora.FlowStudio.Infrastructure;
using Aurora.FlowStudio.Infrastructure.Implementations;
using Aurora.FlowStudio.Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // ✅ In-Memory Cache (works out of the box)
        services.AddMemoryCache();

        // ✅ Optional: Add Redis only if you need distributed caching
        // Uncomment and install package: Microsoft.Extensions.Caching.StackExchangeRedis
        /*
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "FlowStudio_";
        });
        */

        // ✅ Register services
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IDatabaseFactory, DatabaseFactory>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<,>), typeof(RepositoryBase<,>));

        return services;
    }
}