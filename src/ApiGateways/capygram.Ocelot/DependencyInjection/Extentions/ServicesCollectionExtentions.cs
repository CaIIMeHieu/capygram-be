using capygram.Common.Exceptions;
using capygram.Ocelot.DependencyInjection.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace capygram.Ocelot.DependencyInjection.Extentions
{
    public static class ServicesCollectionExtentions
    {
       public static IServiceCollection AddJwtAuthentication( this IServiceCollection services , IConfiguration configuration)
       {
            var jwtOptions = new JwtOptions();
            configuration.GetRequiredSection("JwtOptions").Bind(jwtOptions);
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.SaveToken = true;
                option.RequireHttpsMetadata = false;
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };
                option.Events = new JwtBearerEvents
                {

                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        if (!string.IsNullOrEmpty(accessToken)
                            )
                        {

                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    },
                    OnForbidden = context =>
                    {
                        // Custom behavior when access is forbidden
                        throw new ForbiddenException("User is forbiddened");
                    },
                    OnChallenge = context =>
                    {
                        // Custom behavior when challenging the user
                        throw new UnAuthorizedExeption("User is not authenticated");
                    },

                };
            });
            return services;
       }
    }
}
