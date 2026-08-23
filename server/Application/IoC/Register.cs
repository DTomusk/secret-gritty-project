using Application.Auth.IoC;
using Application.CalendarEvents.IoC;
using Application.Members.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IoC;

public static class Register
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAuthApplicationServices();
        services.AddCalendarEventApplicationServices();
        services.AddMembersApplicationServices();

        return services;
    }

    public static IServiceCollection AddEventHandlers(this IServiceCollection services)
    {
        return services;
    }
}
