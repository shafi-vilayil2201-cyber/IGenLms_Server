namespace IGenServer.Domain.Entities;

public sealed class StudentFocusSession
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Topic { get; set; } = string.Empty;

    public int PlannedMinutes { get; set; }

    public DateTime StartedAtUtc { get; set; }

    public DateTime ExpectedEndAtUtc { get; set; }

    public DateTime? CompletedAtUtc { get; set; }

    public string Status { get; set; } = "active";

    public int ExtensionCount { get; set; }

    public string Source { get; set; } = "web";

    public User? User { get; set; }
}
