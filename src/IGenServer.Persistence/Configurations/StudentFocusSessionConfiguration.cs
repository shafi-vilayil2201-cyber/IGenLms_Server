using IGenServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IGenServer.Persistence.Configurations;

public sealed class StudentFocusSessionConfiguration : IEntityTypeConfiguration<StudentFocusSession>
{
    public void Configure(EntityTypeBuilder<StudentFocusSession> builder)
    {
        builder.ToTable("StudentFocusSessions");

        builder.HasKey(session => session.Id);

        builder.Property(session => session.Topic)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(session => session.PlannedMinutes)
            .IsRequired();

        builder.Property(session => session.StartedAtUtc)
            .IsRequired();

        builder.Property(session => session.ExpectedEndAtUtc)
            .IsRequired();

        builder.Property(session => session.Status)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(session => session.Source)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasOne(session => session.User)
            .WithMany()
            .HasForeignKey(session => session.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(session => new { session.UserId, session.Status });

        builder.HasIndex(session => session.ExpectedEndAtUtc);
    }
}