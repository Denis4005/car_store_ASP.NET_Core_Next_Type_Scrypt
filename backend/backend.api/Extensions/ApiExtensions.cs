using System.Text;
using backend.core.Abstraction;
using backend.core.Enums;
using backend.infrastructure;
using Backend.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace backend.api.Extensions;

public static class ApiExptensions
{
    public static IServiceCollection AddApiAuthentication(this IServiceCollection services,
    IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>()!;
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
            };
        });

        services.AddAuthorization(options =>
        {
            foreach (var permission in Enum.GetValues<Permission>())
            {
                options.AddPolicy($"Permissions:{permission}", 
                    policy => policy.RequireAuthenticatedUser().AddRequirements(new PermissionRequirement([permission])));
            }

        });

        return services;
    }
}