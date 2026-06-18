using IGenServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IGenServer.Persistence.Configurations;

public sealed class StudentDailyScoreConfiguration : IEntityTypeConfiguration<StudentDailyScore>
{
    public void Configure(EntityTypeBuilder<StudentDailyScore> builder)
    {
        builder.ToTable("StudentDailyScores");

        builder.HasKey(score => score.Id);

        builder.Property(score => score.Date)
            .IsRequired();

        builder.Property(score => score.SourceType)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(score => score.Points)
            .IsRequired();

        builder.Property(score => score.StudyMinutes)
            .IsRequired();

        builder.Property(score => score.MetadataJson)
            .HasMaxLength(2000);

        builder.Property(score => score.CreatedAtUtc)
            .IsRequired();

        builder.HasOne(score => score.User)
            .WithMany()
            .HasForeignKey(score => score.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(score => new { score.UserId, score.Date });

        builder.HasIndex(score => score.SourceType);
    }
}