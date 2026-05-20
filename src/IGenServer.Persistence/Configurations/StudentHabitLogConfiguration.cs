using IGenServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IGenServer.Persistence.Configurations;

public sealed class StudentHabitLogConfiguration : IEntityTypeConfiguration<StudentHabitLog>
{
    public void Configure(EntityTypeBuilder<StudentHabitLog> builder)
    {
        builder.ToTable("StudentHabitLogs");

        builder.HasKey(log => log.Id);

        builder.Property(log => log.Date)
            .IsRequired();

        builder.Property(log => log.CompletedMinutes)
            .IsRequired();

        builder.Property(log => log.Note)
            .HasMaxLength(500);

        builder.Property(log => log.Source)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(log => log.CreatedAtUtc)
            .IsRequired();

        builder.HasOne(log => log.User)
            .WithMany()
            .HasForeignKey(log => log.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(log => log.StudentHabit)
            .WithMany()
            .HasForeignKey(log => log.StudentHabitId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(log => new { log.StudentHabitId, log.Date })
            .IsUnique();
    }
}