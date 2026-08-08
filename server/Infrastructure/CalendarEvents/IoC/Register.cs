using Application.CalendarEvents.Interfaces;
using Infrastructure.CalendarEvents.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CalendarEvents.IoC;

public static class Register
{
    public static IServiceCollection AddCalendarEventInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ICalendarEventRepository, CalendarEventRespository>();

        return services;
    }
}
