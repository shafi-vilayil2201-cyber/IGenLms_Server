using IGenServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IGenServer.Persistence.Configurations;

public sealed class MentorProfileConfiguration : IEntityTypeConfiguration<MentorProfile>
{
    public void Configure(EntityTypeBuilder<MentorProfile> builder)
    {
        builder.ToTable("MentorProfiles");

        builder.HasKey(x => x.UserId);

        builder.Property(x => x.ExpertiseJson)
            .IsRequired();

        builder.Property(x => x.ApprovalStatus)
            .IsRequired();
    }
}
