using Domain.Auth.Entities;
using Domain.CalendarEvents.Entities;
using Domain.Shared.Events;
using Infrastructure.Auth.EF;
using Infrastructure.Shared.EF;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    #region auth
    public DbSet<User> Users => Set<User>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<Role> Roles => Set<Role>();
    #endregion

    #region calendar events
    public DbSet<CalendarEvent> CalendarEvents => Set<CalendarEvent>();
    #endregion

    #region events
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    public DbSet<ProcessedEvent> ProcessedEvents => Set<ProcessedEvent>();
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureAuthContext();
        modelBuilder.ConfigureSharedContext();
    }
}
