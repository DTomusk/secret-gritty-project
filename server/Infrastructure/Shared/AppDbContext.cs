using Domain.Auth.Entities;
using Domain.Auth.ValueObjects;
using Domain.Shared.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Shared;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    public DbSet<ProcessedEvent> ProcessedEvents => Set<ProcessedEvent>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var registrationCodeConverter = new ValueConverter<RegistrationCode, string>(
            v => v.ToString(),
            v => RegistrationCode.Create(v).Value);

        // Configure User entity
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(100);
            entity.HasIndex(e => e.UserName)
                .IsUnique();
            entity.Property(e => e.PasswordHash)
                .IsRequired();
            entity.Property(e => e.CreatedAt)
                .IsRequired();
            entity.Property(e => e.RegistrationCode)
                .IsRequired()
                .HasConversion(registrationCodeConverter);
        });

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
