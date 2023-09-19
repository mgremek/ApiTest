using Api_Cats;
using Api_Cats.Api;
using Api_Cats.Entities;
using Api_Cats.Entities.Validators;
using Api_Cats.Middleware;
using Api_Cats.Services;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// NLog: Setup NLog for Dependency injection
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Host.UseNLog();

// Add services to the container.

builder.Services.AddControllers()
    .AddFluentValidation();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CatsDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CatsDbContext")));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddScoped<ICatsService, CatsService>();
builder.Services.AddScoped<ICatsQlService, CatsQLService>();

builder.Services.AddScoped<IValidator<Product>, ProductValidator>();

//builder.Services.AddIdentity<IdentityUser, IdentityRole>(
//        options => {
//            options.SignIn.RequireConfirmedAccount = false;
//            options.Password.RequiredLength = 3;
//            options.Password.RequiredUniqueChars = 0;
//            options.Password.RequireLowercase = false;
//            options.Password.RequireUppercase = false;
//            options.Password.RequireDigit = false;
//            options.Password.RequireNonAlphanumeric = false;
//        }
//        )
//    .AddEntityFrameworkStores<CatsDbContext>();

var authenticationSettings = new AuthenticationSettings();

builder.Configuration.GetSection("JWT").Bind(authenticationSettings);
builder.Services.AddSingleton(authenticationSettings);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.ValidAudience,
        ValidAudience = authenticationSettings.ValidIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.Secret))
    };
});

// Register services directly with Autofac here.
// Don't call builder.Populate(), that happens in AutofacServiceProviderFactory.
builder.Host.ConfigureContainer<ContainerBuilder>(
   builder => builder.RegisterModule(new AutofacModule()));

builder.Services.AddScoped<CatsSeeder>();
builder.Services.AddScoped<RequestLoggingMiddleware>();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>();

var app = builder.Build();

var scope = app.Services.CreateScope();
var restaurantSeeder = scope.ServiceProvider.GetRequiredService<CatsSeeder>();
restaurantSeeder.Seed();

// Configure the HTTP request pipeline.

app.UseAuthentication();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapGraphQL();

app.Run();