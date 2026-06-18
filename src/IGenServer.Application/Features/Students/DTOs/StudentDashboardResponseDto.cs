namespace IGenServer.Application.Features.Students.DTOs;

public sealed class StudentDashboardResponseDto
{
    public int UserId { get; init; }

    public string FullName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public int TargetYear { get; init; }

    public int StudyStreak { get; init; }

    public int Rank { get; init; }

    public int EnrolledCoursesCount { get; init; }
}