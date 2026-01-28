using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Aurora.FlowStudio.Data.Context;
using Aurora.FlowStudio.Entity.Entity.Identity;

namespace Aurora.FlowStudio.Data.Extensions
{
    /// <summary>
    /// Extension methods for registering Data layer services
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers FlowStudioDbContext with SQL Server and ASP.NET Core Identity
        /// </summary>
        public static IServiceCollection AddFlowStudioData(
            this IServiceCollection services,
            IConfiguration configuration,
            string connectionStringName = "DefaultConnection")
        {
            var connectionString = configuration.GetConnectionString(connectionStringName)
                ?? throw new InvalidOperationException($"Connection string '{connectionStringName}' not found.");

            // Register DbContext
            services.AddDbContext<FlowStudioDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(FlowStudioDbContext).Assembly.FullName);
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                    sqlOptions.CommandTimeout(120);
                });

                // Enable sensitive data logging in development
                if (configuration.GetValue<string>("Environment") == "Development")
                {
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                }
            });

            // Register ASP.NET Core Identity
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 4;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

                // Sign-in settings
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<FlowStudioDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }

        /// <summary>
        /// Registers FlowStudioDbContext with custom tenant resolution
        /// </summary>
        public static IServiceCollection AddFlowStudioDataWithTenant(
            this IServiceCollection services,
            IConfiguration configuration,
            Func<IServiceProvider, Guid?> tenantIdResolver,
            string connectionStringName = "DefaultConnection")
        {
            var connectionString = configuration.GetConnectionString(connectionStringName)
                ?? throw new InvalidOperationException($"Connection string '{connectionStringName}' not found.");

            // Register DbContext with tenant resolution
            services.AddDbContext<FlowStudioDbContext>((serviceProvider, options) =>
            {
                var tenantId = tenantIdResolver(serviceProvider);

                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(FlowStudioDbContext).Assembly.FullName);
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                    sqlOptions.CommandTimeout(120);
                });

                if (configuration.GetValue<string>("Environment") == "Development")
                {
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                }
            });

            // Register ASP.NET Core Identity
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 4;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<FlowStudioDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}
