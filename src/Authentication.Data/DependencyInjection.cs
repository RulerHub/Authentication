using Authentication.Data.Context;
using Authentication.Data.Repositories.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Authentication.Data.Services.Users.Interface;
using Authentication.Data.Services.Users.Implement;
using Authentication.Data.Repositories.Users.Interfaces;
using Authentication.Data.Repositories.Users.Implements;

namespace Authentication.Data;

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
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString)
            .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));
        services.AddDatabaseDeveloperPageExceptionFilter();

        return services;
    }
}
