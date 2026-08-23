using Application.CalendarEvents.Commands;
using Application.CalendarEvents.DTOs;
using Application.CalendarEvents.Handlers;
using Application.CalendarEvents.Queries;
using Application.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.CalendarEvents.IoC;

public static class Register
{
    public static IServiceCollection AddCalendarEventApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<ScheduleEventCommand, ScheduleEventResponse>, ScheduleEventCommandHandler>();
        services.AddScoped<ICommandHandler<ChooseNextHostCommand>, ChooseNextHostCommandHandler>();
        services.AddScoped<IQueryHandler<UpcomingEventQuery, UpcomingEventResponse?>, UpcomingEventQueryHandler>();
        services.AddScoped<IQueryHandler<UpcomingBookClubQuery, UpcomingEventResponse?>, UpcomingBookClubQueryHandler>();
        services.AddScoped<IQueryHandler<EventByIdQuery, EventDetailResponse?>, EventByIdQueryHandler>();
        services.AddScoped<ICommandHandler<CreateEventPollCommand, CreateEventPollResponse>, CreateEventPollCommandHandler>();

        return services;
    }
}
