using IGenServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IGenServer.Persistence.Configurations;

public sealed class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder.ToTable("Subjects");

        builder.HasKey(subject => subject.Id);

        builder.Property(subject => subject.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(subject => subject.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(subject => subject.DurationMonths)
            .IsRequired();

        builder.Property(subject => subject.IsPublished)
            .IsRequired();

        builder.HasOne(subject => subject.Program)
            .WithMany(program => program.Subjects)
            .HasForeignKey(subject => subject.ProgramId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
