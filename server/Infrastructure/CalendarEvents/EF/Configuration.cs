using Domain.CalendarEvents.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.CalendarEvents.EF;

public static class Configuration
{
    public static void ConfigureCalendarEventsContext(this ModelBuilder modelBuilder)
    {
        // Configure CalendarEvent entity
        modelBuilder.Entity<CalendarEvent>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
    }
}
