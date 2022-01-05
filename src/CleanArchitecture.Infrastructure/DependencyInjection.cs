using CleanArchitecture.Core.Projects;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddTransient<IProjectRepository, ProjectRepository>();
        services.AddSingleton<IClock, SystemClock>();
        
        return services;
    }
}
