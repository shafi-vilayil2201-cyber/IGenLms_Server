namespace IGenServer.Domain.Entities;

public sealed class StudentHabitLog
{
    public int Id { get; set; }

    public int StudentHabitId { get; set; }

    public int UserId { get; set; }

    public DateOnly Date { get; set; }

    public bool IsCompleted { get; set; }

    public int CompletedMinutes { get; set; }

    public string? Note { get; set; }

    public string Source { get; set; } = "web";

    public DateTime? CompletedAtUtc { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public StudentHabit? StudentHabit { get; set; }

    public User? User { get; set; }
}