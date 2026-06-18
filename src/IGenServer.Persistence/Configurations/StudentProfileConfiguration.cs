using IGenServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IGenServer.Persistence.Configurations;

public sealed class StudentProfileConfiguration : IEntityTypeConfiguration<StudentProfile>
{
    public void Configure(EntityTypeBuilder<StudentProfile> builder)
    {
        builder.ToTable("StudentProfiles");

        builder.HasKey(x => x.UserId);

        builder.Property(x => x.TargetYear)
            .IsRequired();

        builder.Property(x => x.StudyStreak)
            .IsRequired();

        builder.Property(x => x.Rank)
            .IsRequired();
    }
}
