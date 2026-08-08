using Domain.Auth.Entities;
using Domain.Auth.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Auth.EF;

public static class Configuration
{
    public static void ConfigureAuthContext(this ModelBuilder modelBuilder)
    {
        // Configure UserRole entity
        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("UserRoles");
            entity.HasKey(ur => new { ur.UserId, ur.RoleId });

            entity.HasOne(x => x.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(x => x.AssignedAt)
                .IsRequired();
        });

        // Configure Role entity
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);
            entity.HasIndex(r => r.Name)
                .IsUnique();
        });

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
    }
}
