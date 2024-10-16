using capygram.Common.Shared;
using capygram.Newsfeed.DependencyInjection.Options;
using capygram.Newsfeed.Domain.Data;
using capygram.Newsfeed.Domain.Repositories;
using capygram.Newsfeed.Repositories;
using capygram.Newsfeed.Services;
using capygram.NewsFeed.DependencyInjection.Extentions;
using MassTransit;
using RabbitMQ.Client;
using System.Reflection;

namespace capygram.Newsfeed.DependencyInjection.Extentions
{
    public static class ServicesCollectionExtentions
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<NewsfeedDBSetting>(
                configuration.GetSection("NewsfeedStoreDatabase")
                );
            return services;
        }
        public static IServiceCollection ConfigurationMediatR(this IServiceCollection services)
        {
            services.AddMediatR(cgf => cgf.RegisterServicesFromAssembly(typeof(ServicesCollectionExtentions).Assembly));
            return services;

        }
        public static IServiceCollection AddServicesCollection(this IServiceCollection services)
        {
            services.AddSingleton<INewsfeedContext, NewsfeedContext>();
            services.AddScoped<INewsfeedRepositories, NewsfeedRepositories>();
            services.AddScoped<INewsfeedService, NewsfeedService>();
            return services;
        }
        public static IServiceCollection AddMasstransitConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var massOp = new MasstransitOptions();
            configuration.GetRequiredSection("MasstransitOptions").Bind(massOp);
            services.AddMassTransit(cfg =>
            {
                cfg.AddConsumers(Assembly.GetExecutingAssembly());
                cfg.SetKebabCaseEndpointNameFormatter();
                cfg.UsingRabbitMq((context, bus) =>
                {
                    bus.Host(massOp.Host, massOp.VHost, host =>
                    {
                        host.Username(massOp.UserName);
                        host.Password(massOp.Password);
                    });
                    bus.MessageTopology.SetEntityNameFormatter(new KebabCaseEntityNameFormatter());
                    bus.ConfigureEndpoints(context);
                });
            });
            return services;
        }
    }
}

