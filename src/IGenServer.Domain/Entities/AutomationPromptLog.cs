namespace IGenServer.Domain.Entities;

public sealed class AutomationPromptLog
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string PromptType { get; set; } = string.Empty;

    public DateOnly Date { get; set; }

    public string MessageText { get; set; } = string.Empty;

    public string Status { get; set; } = "pending";

    public DateTime SentAtUtc { get; set; }

    public DateTime? ResponseReceivedAtUtc { get; set; }

    public string? TelegramChatId { get; set; }

    public User? User { get; set; }
}