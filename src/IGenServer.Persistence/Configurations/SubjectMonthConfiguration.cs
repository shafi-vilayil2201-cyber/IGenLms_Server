using IGenServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IGenServer.Persistence.Configurations;

public sealed class SubjectMonthConfiguration : IEntityTypeConfiguration<SubjectMonth>
{
    public void Configure(EntityTypeBuilder<SubjectMonth> builder)
    {
        builder.ToTable("SubjectMonths");

        builder.HasKey(month => month.Id);

        builder.Property(month => month.MonthNumber)
            .IsRequired();

        builder.Property(month => month.Title)
            .HasMaxLength(150)
            .IsRequired();

        builder.HasIndex(month => new { month.SubjectId, month.MonthNumber })
            .IsUnique();

        builder.HasOne(month => month.Subject)
            .WithMany(subject => subject.Months)
            .HasForeignKey(month => month.SubjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
