using IGenServer.Domain.Enums;

namespace IGenServer.Domain.Entities;

public sealed class StudentHabit
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public StudentHabitType HabitType { get; set; } = StudentHabitType.Study;

    public string ReminderTime { get; set; } = "06:30";

    public bool IsReminderEnabled { get; set; } = true;

    public bool IsActive { get; set; } = true;

    public int TargetMinutes { get; set; }

    public int Points { get; set; } = 10;

    public DateTime CreatedAtUtc { get; set; }

    public DateTime UpdatedAtUtc { get; set; }

    public User? User { get; set; }
}