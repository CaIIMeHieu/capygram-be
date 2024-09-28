using capygram.Chat.DependencyInjection.Options;
using capygram.Chat.Domain.Data;
using capygram.Chat.Domain.Repositories;
using capygram.Chat.Repositories;
using capygram.Chat.Services;

namespace capygram.Chat.DependencyInjection.Extentions
{
    public static class ServicesCollectionExtentions
    {
        public static IServiceCollection AddConfiguration( this IServiceCollection services ,IConfiguration configuration )
        {
            services.Configure<ChatDBSetting>(
                configuration.GetSection("ChatDBSetting")
                );
            return services;
        }

        public static IServiceCollection AddServicesCollection( this IServiceCollection services )
        {
            services.AddSingleton<IChatContext, ChatContext>();
            services.AddScoped<IChatRepositories, ChatRepositories>();
            services.AddScoped<IChatServices, ChatServices>();
            return services;
        }

    }
}
