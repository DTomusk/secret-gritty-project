using Application.Auth.Commands;
using Application.Auth.DTOs;
using Application.Auth.Handlers;
using Application.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Auth.IoC;

public static class Register
{
    public static IServiceCollection AddAuthApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<LoginUserCommand, AuthResponse>, LoginUserCommandHandler>();
        services.AddScoped<ICommandHandler<RegisterUserCommand, AuthResponse>, RegisterUserCommandHandler>();
        services.AddScoped<ICommandHandler<CreateUserCommand, CreateUserResponse>, CreateUserCommandHandler>();

        return services;
    }
}
