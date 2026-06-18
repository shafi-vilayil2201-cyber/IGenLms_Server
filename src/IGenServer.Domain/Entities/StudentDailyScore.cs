namespace IGenServer.Domain.Entities;

public sealed class StudentDailyScore
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateOnly Date { get; set; }

    public string SourceType { get; set; } = string.Empty;

    public int Points { get; set; }

    public int StudyMinutes { get; set; }

    public string? MetadataJson { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public User? User { get; set; }
}
