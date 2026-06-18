using IGenServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IGenServer.Persistence.Configurations;

public sealed class AutomationPromptLogConfiguration : IEntityTypeConfiguration<AutomationPromptLog>
{
    public void Configure(EntityTypeBuilder<AutomationPromptLog> builder)
    {
        builder.ToTable("AutomationPromptLogs");

        builder.HasKey(prompt => prompt.Id);

        builder.Property(prompt => prompt.PromptType)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(prompt => prompt.Date)
            .IsRequired();

        builder.Property(prompt => prompt.MessageText)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(prompt => prompt.Status)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(prompt => prompt.SentAtUtc)
            .IsRequired();

        builder.Property(prompt => prompt.TelegramChatId)
            .HasMaxLength(100);

        builder.HasOne(prompt => prompt.User)
            .WithMany()
            .HasForeignKey(prompt => prompt.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(prompt => new { prompt.UserId, prompt.Date, prompt.PromptType });

        builder.HasIndex(prompt => prompt.Status);
    }
}