using IGenServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IGenServer.Persistence.Configurations;

public sealed class ProgramConfiguration : IEntityTypeConfiguration<Program>
{
    public void Configure(EntityTypeBuilder<Program> builder)
    {
        builder.ToTable("Programs");

        builder.HasKey(program => program.Id);

        builder.Property(program => program.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(program => program.Code)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(program => program.IsActive)
            .IsRequired();

        builder.HasIndex(program => program.Code)
            .IsUnique();
    }
}
