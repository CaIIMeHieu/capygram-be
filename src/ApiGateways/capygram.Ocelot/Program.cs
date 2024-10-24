using capygram.Ocelot.DependencyInjection.Extentions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("ocelot.production.json").Build();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});
builder.Services.AddControllers();
//builder.Services.AddJwtAuthentication(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOcelot(configuration);
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI( c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
//}

app.UseHttpsRedirection();

//app.UseAuthentication();

app.UseCors("AllowSpecificOrigin");
app.UseWebSockets();
app.UseOcelot().Wait();

//app.UseAuthorization();

app.MapControllers();

app.Run();
