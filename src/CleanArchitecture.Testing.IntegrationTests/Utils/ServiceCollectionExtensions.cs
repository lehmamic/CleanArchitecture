using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Testing.IntegrationTests.Utils;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ReplaceTransient<TService, TImplementation>(this IServiceCollection services)
            where TImplementation : class, TService
        {
            services.RemoveService<TService>();
            services.AddTransient(typeof(TService), typeof(TImplementation));

            return services;
        }

        public static IServiceCollection ReplaceScoped<TService, TImplementation>(this IServiceCollection services)
            where TImplementation : class, TService
        {
            services.RemoveService<TService>();
            services.AddScoped(typeof(TService), typeof(TImplementation));

            return services;
        }

        public static IServiceCollection ReplaceSingleton<TService, TImplementation>(this IServiceCollection services)
            where TImplementation : class, TService
        {
            services.RemoveService<TService>();
            services.AddSingleton(typeof(TService), typeof(TImplementation));

            return services;
        }

        public static IServiceCollection ReplaceSingleton<TService>(this IServiceCollection services, TService instance)
            where TService : class
        {
            services.RemoveService<TService>();
            services.AddSingleton(instance);

            return services;
        }

        public static IServiceCollection ReplaceSingleton<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory)
            where TService : class
        {
            services.RemoveService<TService>();
            services.AddSingleton(implementationFactory);

            return services;
        }

        public static IServiceCollection RemoveService<TService>(this IServiceCollection services)
        {
            if (services.Any(x => x.ServiceType == typeof(TService)))
            {
                var serviceDescriptors = services.Where(x => x.ServiceType == typeof(TService)).ToList();
                foreach (var serviceDescriptor in serviceDescriptors)
                {
                    services.Remove(serviceDescriptor);
                }
            }

            return services;
        }

        public static IServiceCollection RemoveService(this IServiceCollection services, string typeName)
        {
            if (services.Any(x => x.ServiceType.Name == typeName))
            {
                var serviceDescriptors = services.Where(x => x.ServiceType.Name == typeName).ToList();
                foreach (var serviceDescriptor in serviceDescriptors)
                {
                    services.Remove(serviceDescriptor);
                }
            }

            return services;
        }

        public static IServiceCollection ReplaceDbContext<TContext>(
            this IServiceCollection services,
            Action<DbContextOptionsBuilder>? optionsAction = null,
            ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
            ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
            where TContext : DbContext
        {
            services.RemoveService<TContext>();
            services.RemoveService<DbContextOptions>();
            services.RemoveService<DbContextOptions<TContext>>();
            services.AddDbContext<TContext>(optionsAction, contextLifetime, optionsLifetime);

            return services;
        }
    }
