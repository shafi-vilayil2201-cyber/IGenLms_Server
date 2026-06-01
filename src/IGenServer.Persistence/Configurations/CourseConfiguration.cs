using IGenServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IGenServer.Persistence.Configurations;

public sealed class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");

        builder.HasKey(course => course.Id);

        builder.Property(course => course.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(course => course.Description)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(course => course.DurationMonths)
            .IsRequired();

        builder.Property(course => course.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(course => course.Status)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasOne(course => course.Program)
            .WithMany(program => program.Courses)
            .HasForeignKey(course => course.ProgramId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
