using Application.Auth.Commands;
using Application.Auth.DTOs;
using Application.Auth.Handlers;
using Application.CalendarEvents.Commands;
using Application.CalendarEvents.DTOs;
using Application.CalendarEvents.Handlers;
using Application.CalendarEvents.Queries;
using Application.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IoC;

public static class Register
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // TODO: split registration by bounded context
        // Auth handlers
        services.AddScoped<ICommandHandler<LoginUserCommand, AuthResponse>, LoginUserCommandHandler>();
        services.AddScoped<ICommandHandler<RegisterUserCommand, AuthResponse>, RegisterUserCommandHandler>();
        services.AddScoped<ICommandHandler<CreateUserCommand, CreateUserResponse>, CreateUserCommandHandler>();

        // CalendarEvent handlers
        services.AddScoped<ICommandHandler<ScheduleEventCommand, ScheduleEventResponse>, ScheduleEventCommandHandler>();
        services.AddScoped<IQueryHandler<UpcomingEventQuery, UpcomingEventResponse?>, UpcomingEventQueryHandler>();
        services.AddScoped<IQueryHandler<EventByIdQuery, EventDetailResponse?>, EventByIdQueryHandler>();
        return services;
    }

    public static IServiceCollection AddEventHandlers(this IServiceCollection services)
    {
        return services;
    }
}
