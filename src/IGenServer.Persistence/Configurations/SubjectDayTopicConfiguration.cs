using IGenServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IGenServer.Persistence.Configurations;

public sealed class SubjectDayTopicConfiguration : IEntityTypeConfiguration<SubjectDayTopic>
{
    public void Configure(EntityTypeBuilder<SubjectDayTopic> builder)
    {
        builder.ToTable("SubjectDayTopics");

        builder.HasKey(dayTopic => dayTopic.Id);

        builder.Property(dayTopic => dayTopic.DayNumber)
            .IsRequired();

        builder.Property(dayTopic => dayTopic.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(dayTopic => dayTopic.Description)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(dayTopic => dayTopic.EstimatedMinutes)
            .IsRequired();

        builder.HasIndex(dayTopic => new { dayTopic.SubjectWeekId, dayTopic.DayNumber })
            .IsUnique();

        builder.HasOne(dayTopic => dayTopic.SubjectWeek)
            .WithMany(week => week.DayTopics)
            .HasForeignKey(dayTopic => dayTopic.SubjectWeekId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
