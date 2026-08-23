using Application.Members.DTOs;
using Application.Members.Handlers;
using Application.Members.Queries;
using Application.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Members.IoC;

public static class Register
{
    public static IServiceCollection AddMembersApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IQueryHandler<GetMembersQuery, IEnumerable<MemberDTO>>, GetMembersQueryHandler>();

        return services;
    }
}
