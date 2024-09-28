using capygram.Common.Abstraction;
using capygram.Common.Middlewares;
using capygram.Media.DependencyInjection.Options;
using capygram.Media.Service;
using CloudinaryDotNet;
using MassTransit;
using RabbitMQ.Client;
using System.Reflection;

namespace capygram.Media.DependencyInjection.Extentions
{
    public static class ServicesCollectionExtentions
    {
        public static IServiceCollection AddConfigOptions( this IServiceCollection service , IConfiguration configuration)
        {
            service.Configure<MasstransitOptions>(configuration.GetSection("MasstransitOptions"));
            return service;
        }

        public static IServiceCollection ConfigurationServices(this IServiceCollection services)
        {
            services.AddScoped<IMediaService, MediaService>();
            return services;
        }

        public static IServiceCollection ConfigurationCloudinary(this IServiceCollection services,IConfiguration configuration)
        {
            var cloudinaryOptions = new CloudinaryOptions();
            configuration.GetRequiredSection("CloudinaryOptions").Bind(cloudinaryOptions);
            var cloudinary = new Cloudinary(cloudinaryOptions.ApiUrl);
            cloudinary.Api.Secure = true;
            services.AddSingleton(cloudinary);
            return services;
        }
        public static IServiceCollection ConfigurationMasstransit(this IServiceCollection services, IConfiguration config)
        {
            var massOp = new MasstransitOptions();
            config.GetRequiredSection("MasstransitOptions").Bind(massOp);
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

                    //topic exchange
                    //set entity for routing
                    bus.Message<INotification>(e => e.SetEntityName(massOp.ExchangeName));

                    bus.Publish<INotification>(e =>
                    {
                        e.Durable = true; // default true
                        e.AutoDelete = false; // default: false
                        e.ExchangeType = ExchangeType.Topic;
                    });
                    // config routing
                    bus.Send<INotification>(e =>
                    {
                        // use Type field of message for routing key: user | post | 
                        e.UseRoutingKeyFormatter(context => context.Message.Type.ToString());
                    });

                    bus.MessageTopology.SetEntityNameFormatter(new KebabCaseEntityNameFormatter());

                    //config for queue and consumer
                    bus.ConfigureEndpoints(context);
                });
            });
            return services;
        }
    }
}
