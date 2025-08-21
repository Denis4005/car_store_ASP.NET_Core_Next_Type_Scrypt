using backend.application.Services;
using backend.core.Abstraction;
using backend.infrastructure;
using Backend.Application.Services;
using Backend.Core.Abstraction;
using Backend.Dataaccess.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace backend.api.Extensions;

public static class ServiceExptensions
{
    public static IServiceCollection AddAppService(this IServiceCollection services)
    {
        services.AddScoped<ICarsService, CarsService>();
        services.AddScoped<ICarsRepository, CarsRepository>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddSingleton<IJwtProvider, JwtProvider>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IPermissionService,PermissionService>();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        
        return services;
    }
}