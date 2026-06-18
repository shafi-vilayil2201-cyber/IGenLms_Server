using IGenServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IGenServer.Persistence.Configurations;

public sealed class CourseSubjectConfiguration : IEntityTypeConfiguration<CourseSubject>
{
    public void Configure(EntityTypeBuilder<CourseSubject> builder)
    {
        builder.ToTable("CourseSubjects");

        builder.HasKey(courseSubject => courseSubject.Id);

        builder.Property(courseSubject => courseSubject.DisplayOrder)
            .IsRequired();

        builder.Property(courseSubject => courseSubject.StartMonth)
            .IsRequired();

        builder.HasIndex(courseSubject => new { courseSubject.CourseId, courseSubject.SubjectId })
            .IsUnique();

        builder.HasOne(courseSubject => courseSubject.Course)
            .WithMany(course => course.CourseSubjects)
            .HasForeignKey(courseSubject => courseSubject.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(courseSubject => courseSubject.Subject)
            .WithMany(subject => subject.CourseSubjects)
            .HasForeignKey(courseSubject => courseSubject.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
