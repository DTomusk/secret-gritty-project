using Domain.Auth.Entities;
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
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Date).IsRequired();
            entity.Property(e => e.EventType).IsRequired();
            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.ScheduledByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
