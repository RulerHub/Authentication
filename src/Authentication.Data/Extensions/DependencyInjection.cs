using Authentication.Data.Context;
using Authentication.Data.Repositories.Generic;
using Authentication.Data.Repositories.Users.Implements;
using Authentication.Data.Repositories.Users.Interfaces;
using Authentication.Data.Services.Users.Implement;
using Authentication.Data.Services.Users.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Authentication.Data.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddServicesApplication(this IServiceCollection services)
    {
        // TODO: Add services here
        //services.AddLocalization();
        services.AddControllers();
        services.AddScoped<IUserService, UserService>();

        return services;
    }

    public static IServiceCollection AddRepositoryApplication(this IServiceCollection services)
    {
        // TODO: Add Repository here
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Db context configuration
        var connectionString = configuration
            .GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString)
            .ConfigureWarnings(warnings => warnings
            .Ignore(RelationalEventId.PendingModelChangesWarning)));
        services.AddDatabaseDeveloperPageExceptionFilter();

        return services;
    }

    public static IServiceCollection AddAuthenticationApplication(this IServiceCollection services,
        IConfiguration configuration)
    {
        // TODO: Add Authentication service here

        // Cookie-based authentication configuration
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        }).AddCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(configuration
                .GetSection("JwtConfig")
                .GetValue<int>("ExpirationMinutes"));
            options.SlidingExpiration = true;
            options.Cookie.Name = "session";
            options.Cookie.SameSite = SameSiteMode.Lax;
            options.Cookie.Domain = configuration["AllowedHosts"] ?? string.Empty;
            options.Cookie.Path = "/";
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            };

            options.Events.OnRedirectToAccessDenied = context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Task.CompletedTask;
            };
        });

        return services;
    }

    public static IServiceCollection AddCORSApplication(this
        IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment Environment)
    {
        // TODO: Add CORS here
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin", corsPolicyBuilder =>
            {
                if (Environment.IsDevelopment())
                {
                    corsPolicyBuilder
                        .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                }
                else
                {
                    corsPolicyBuilder
                        .WithOrigins(configuration["AllowedHosts"] ?? string.Empty)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                }
            });
        });

        return services;
    }
}
