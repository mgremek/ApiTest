using Api_Cats;
using Api_Cats.Entities;
using Api_Cats.Middleware;
using Api_Cats.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using NLog.Web;


var builder = WebApplication.CreateBuilder(args);

// NLog: Setup NLog for Dependency injection
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Host.UseNLog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CatsDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CatsDbContext")));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddScoped<ICatsService, CatsService>();

// Register services directly with Autofac here.
// Don't call builder.Populate(), that happens in AutofacServiceProviderFactory.
builder.Host.ConfigureContainer<ContainerBuilder>(
   builder => builder.RegisterModule(new AutofacModule()));

builder.Services.AddScoped<CatsSeeder>();
builder.Services.AddScoped<RequestLoggingMiddleware>();

var app = builder.Build();


var scope = app.Services.CreateScope();
var restaurantSeeder = scope.ServiceProvider.GetRequiredService<CatsSeeder>();
restaurantSeeder.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
