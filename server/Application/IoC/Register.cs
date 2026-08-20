using Application.Auth.Commands;
using Application.Auth.DTOs;
using Application.Auth.Handlers;
using Application.CalendarEvents.Commands;
using Application.CalendarEvents.DTOs;
using Application.CalendarEvents.Handlers;
using Application.CalendarEvents.Queries;
using Application.Members.DTOs;
using Application.Members.Handlers;
using Application.Members.Queries;
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
        services.AddScoped<ICommandHandler<ChooseNextHostCommand>, ChooseNextHostCommandHandler>();
        services.AddScoped<IQueryHandler<UpcomingEventQuery, UpcomingEventResponse?>, UpcomingEventQueryHandler>();
        services.AddScoped<IQueryHandler<UpcomingBookClubQuery, UpcomingEventResponse?>, UpcomingBookClubQueryHandler>();
        services.AddScoped<IQueryHandler<EventByIdQuery, EventDetailResponse?>, EventByIdQueryHandler>();

        // Members handlers
        services.AddScoped<IQueryHandler<GetMembersQuery, IEnumerable<MemberDTO>>, GetMembersQueryHandler>();

        return services;
    }

    public static IServiceCollection AddEventHandlers(this IServiceCollection services)
    {
        return services;
    }
}
