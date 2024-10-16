using capygram.Common.Shared;
using capygram.Post.DependencyInjection.Options;
using capygram.Post.Domain.Data;
using capygram.Post.Domain.Repositories;
using capygram.Post.MessageBus.Event;
using capygram.Post.Repositories;
using capygram.Post.Services;
using MassTransit;
using MongoDB.Bson.Serialization.Conventions;
using RabbitMQ.Client;
using StackExchange.Redis;
using System.Reflection;

namespace capygram.Post.DependencyInjection.Extentions
{
    public static class ServicesCollectionExtentions
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PostDBSetting>(
                configuration.GetSection("PostStoreDatabase")
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
            services.AddSingleton<IPostsContext, PostsContext>();
            services.AddScoped<IPostRepositories, PostRepositories>();
            services.AddScoped<IPostService, PostService>();
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
                    //// Receive for user
                    bus.ReceiveEndpoint(massOp.PostQueueName, re =>
                    {
                        re.ConfigureConsumeTopology = false;
                        re.ConfigureConsumer<PostMediaNotificationConsumer>(context);

                        re.Bind(massOp.ExchangeMediaName, e =>
                        {
                            e.Durable = true; // default true
                            e.AutoDelete = false; // default: false
                            e.RoutingKey = NotificationMediaType.post;
                            e.ExchangeType = ExchangeType.Topic;
                        });
                    });
                    bus.MessageTopology.SetEntityNameFormatter(new KebabCaseEntityNameFormatter());
                    bus.ConfigureEndpoints(context);
                });
            });
            return services;
        }
        public static IServiceCollection AddRedisConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConfiguration = new RedisOptions();
            configuration.GetSection("RedisConfiguration").Bind(redisConfiguration);
            services.AddSingleton(redisConfiguration);
            if(!redisConfiguration.Enable)
                return services;

            services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisConfiguration.ConnectionString));
            services.AddStackExchangeRedisCache(option => option.Configuration = redisConfiguration.ConnectionString);
            services.AddSingleton<ICacheService, CacheService>();
            return services;
        }
    }
}
    
