using capygram.Auth.DependencyInjection.Extensions;
using capygram.Auth.Domain.Data;
using capygram.Auth.Domain.Services;
using capygram.Common.DependencyInjection.Extensions;
using capygram.Common.Middlewares;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

// Load appsettings.Auth.json
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.Auth.Development.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173", "https://sd-gilt.vercel.app")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});
// Add services to the container.
Log.Logger = new LoggerConfiguration().ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();
builder.Logging
    .ClearProviders()
    .AddSerilog();

builder.Host.UseSerilog();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddConfigOption(builder.Configuration)
    .AddJwtAuthentication(builder.Configuration)
    .AddServices()
    .AddIdentityHandler()
    .ConfigurationMediatR(typeof(Program).Assembly)
    .ConfigurationMasstransit(builder.Configuration)
    .ConfigurationFluentValidation();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty;
});
//}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var user = scope.ServiceProvider.GetService<IUserContext>().Users;
    var encypter = scope.ServiceProvider.GetService<IEncrypter>();
    await Seeder.Seed(user, encypter);
}
app.Run();
