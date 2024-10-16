using capygram.Common.DependencyInjection.Extensions;
using capygram.Common.Middlewares;
using capygram.Media.DependencyInjection.Extentions;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.Media.Development.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .ConfigurationServices()
    .ConfigurationCloudinary(builder.Configuration)
    .ConfigurationMediatR(typeof(Program).Assembly)
    .AddConfigOptions(builder.Configuration)
    .ConfigurationMasstransit(builder.Configuration);

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
app.UseAuthorization();

app.MapControllers();

app.Run();
