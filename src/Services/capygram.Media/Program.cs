using capygram.Common.DependencyInjection.Extensions;
using capygram.Common.Middlewares;
using capygram.Media.DependencyInjection.Extentions;

var builder = WebApplication.CreateBuilder(args);

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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
