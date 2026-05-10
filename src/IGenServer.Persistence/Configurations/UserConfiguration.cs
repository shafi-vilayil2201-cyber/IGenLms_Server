
using IGenServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IGenServer.Persistance.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x=> x.Id);

        builder.Property(x => x.FullName)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(x=> x.Email)
            .IsRequired()
            .HasMaxLength(256);
        
        builder.HasIndex(x=> x.Email)
            .IsUnique();
        builder.Property(x=> x.PasswordHash)
            .IsRequired();
        builder.Property(x=> x.Role)
            .IsRequired();
            
        builder.HasOne(x=> x.StudentProfile)
            .WithOne(x => x.User)
            .HasForeignKey<StudentProfile>(x=>x.UserId);

        builder.HasOne(x=>x.MentorProfile)
            .WithOne(x => x.User)
            .HasForeignKey<MentorProfile>(x=>x.UserId);
    }
}