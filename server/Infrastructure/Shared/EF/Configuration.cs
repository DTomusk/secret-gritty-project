using Domain.Shared.Events;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Shared.EF;

public static class Configuration
{
    public static void ConfigureSharedContext(this ModelBuilder modelBuilder)
    {
        // Configure OutboxMessage entity
        modelBuilder.Entity<OutboxMessage>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.EventType)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.Payload)
                .IsRequired();
            entity.Property(e => e.OccurredAt)
                .IsRequired();
            entity.HasIndex(e => new { e.ProcessedAt, e.OccurredAt });
        });

        // Configure ProcessedEvent entity
        modelBuilder.Entity<ProcessedEvent>(entity =>
        {
            entity.HasKey(e => new { e.EventId, e.HandlerName });
            entity.Property(e => e.EventId)
                .IsRequired();
            entity.Property(e => e.HandlerName)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.ProcessedAt)
                .IsRequired();
        });
    }
}
