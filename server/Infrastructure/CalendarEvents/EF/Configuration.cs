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
            entity.Property(e => e.Date).IsRequired(false);
            entity.Property(e => e.EventType).IsRequired();
            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.HostUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Poll>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.ClosesAt).IsRequired();

            entity.HasOne<CalendarEvent>()
                .WithMany()
                .HasForeignKey(e => e.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany<PollOption>()
                .WithOne()
                .HasForeignKey(po => po.PollId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PollOption>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.Value).IsRequired();
        });
    }
}
