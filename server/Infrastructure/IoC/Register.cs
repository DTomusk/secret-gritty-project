using Infrastructure.Auth.IoC;
using Infrastructure.CalendarEvents.IoC;
using Infrastructure.Shared.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC;

public static class Register
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthInfrastructureServices(configuration);
        services.AddCalendarEventInfrastructureServices();
        services.AddSharedInfrastructureServices(configuration);

        return services;
    }
}
