using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Data.Extensions;

public static class AuthorizationPolicyExtensions
{

    private const string PermissionClaimType = "permission";

    public static IServiceCollection AddCustomAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("RequireReadUsers", policy =>
                policy.RequireClaim(PermissionClaimType, "users.read"))
            .AddPolicy("RequireWriteUsers", policy =>
                policy.RequireClaim(PermissionClaimType, "users.write"))
            .AddPolicy("RequireDeleteUsers", policy =>
                            policy.RequireClaim(PermissionClaimType, "users.delete"))
            .AddPolicy("RequireReadRoles", policy =>
                            policy.RequireClaim(PermissionClaimType, "roles.read"))
            .AddPolicy("RequireWriteRoles", policy =>
                            policy.RequireClaim(PermissionClaimType, "roles.write"))
            .AddPolicy("RequireDeleteRoles", policy =>
                            policy.RequireClaim(PermissionClaimType, "roles.delete"))
            .AddPolicy("CanManageOwnProfile", policy =>
                policy.RequireClaim(PermissionClaimType, "ReadOwnProfile", "EditOwnProfile"))
            .AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"))
            .AddPolicy("AdminOrCanDelete", policy => policy.RequireAssertion(context =>
                context.User.IsInRole("Admin") ||
                context.User.HasClaim(claim => claim is { Type: PermissionClaimType, Value: "users.delete" })
            ));
        return services;
    }
}