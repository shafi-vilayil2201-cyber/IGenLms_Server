using IGenServer.Domain.Enums;

namespace IGenServer.Application.Features.StudentHabits.DTOs;

public sealed class CreateStudentHabitRequestDto
{
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public StudentHabitType HabitType { get; set; }

    public string ReminderTime { get; set; } = "06:30";

    public bool IsReminderEnabled { get; set; } = true;

    public int TargetMinutes { get; set; }

    public int Points { get; set; } = 10;
}