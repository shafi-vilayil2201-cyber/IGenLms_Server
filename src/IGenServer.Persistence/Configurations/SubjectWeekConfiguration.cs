using IGenServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IGenServer.Persistence.Configurations;

public sealed class SubjectWeekConfiguration : IEntityTypeConfiguration<SubjectWeek>
{
    public void Configure(EntityTypeBuilder<SubjectWeek> builder)
    {
        builder.ToTable("SubjectWeeks");

        builder.HasKey(week => week.Id);

        builder.Property(week => week.WeekNumber)
            .IsRequired();

        builder.Property(week => week.Title)
            .HasMaxLength(150)
            .IsRequired();

        builder.HasIndex(week => new { week.SubjectMonthId, week.WeekNumber })
            .IsUnique();

        builder.HasOne(week => week.SubjectMonth)
            .WithMany(month => month.Weeks)
            .HasForeignKey(week => week.SubjectMonthId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
