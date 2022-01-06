using CleanArchitecture.Core.Projects;
using CleanArchitecture.Infrastructure.Events;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Persistence.Repositories;
using CleanArchitecture.Infrastructure.Security;
using CleanArchitecture.SharedKernel.Auth;
using CleanArchitecture.SharedKernel.Events;
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
                b =>
                {
                    b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                    b.UseNodaTime();
                }));

        services.AddTransient<IProjectRepository, ProjectRepository>();
        services.AddSingleton<IClock>(SystemClock.Instance);
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<IExternalEventProducer, NulloExternalEventProducer>();
        services.AddSingleton<IEventBus, EventBus>();

        services.AddHttpContextAccessor();

        /*
        services.AddAuthentication()
            .AddIdentityServerJwt();

        services.AddAuthorization(options => 
            options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));
            */

        return services;
    }
}
