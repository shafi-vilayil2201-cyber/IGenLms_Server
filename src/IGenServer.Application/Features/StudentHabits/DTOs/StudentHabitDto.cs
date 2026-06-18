namespace IGenServer.Application.Features.StudentHabits.DTOs;

public sealed class StudentHabitDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string HabitType { get; set; } = string.Empty;

    public string ReminderTime { get; set; } = string.Empty;

    public bool IsReminderEnabled { get; set; }

    public bool IsActive { get; set; }

    public int TargetMinutes { get; set; }

    public int Points { get; set; }

    public bool IsCompletedToday { get; set; }

    public int CompletedMinutesToday { get; set; }
}