using IGenServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IGenServer.Persistence.Configurations;

public sealed class StudentHabitConfiguration : IEntityTypeConfiguration<StudentHabit>
{
    public void Configure(EntityTypeBuilder<StudentHabit> builder)
    {
        builder.ToTable("StudentHabits");

        builder.HasKey(habit => habit.Id);

        builder.Property(habit => habit.Title)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(habit => habit.Description)
            .HasMaxLength(500);

        builder.Property(habit => habit.HabitType)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(habit => habit.ReminderTime)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(habit => habit.CreatedAtUtc)
            .IsRequired();

        builder.Property(habit => habit.UpdatedAtUtc)
            .IsRequired();

        builder.HasOne(habit => habit.User)
            .WithMany()
            .HasForeignKey(habit => habit.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}