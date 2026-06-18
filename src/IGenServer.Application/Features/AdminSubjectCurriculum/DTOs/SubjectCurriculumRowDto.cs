namespace IGenServer.Application.Features.AdminSubjectCurriculum.DTOs;

public sealed class SubjectCurriculumRowDto
{
    public int SubjectId { get; init; }
    public string SubjectName { get; init; } = string.Empty;
    public int ProgramId { get; init; }

    public int? MonthId { get; init; }
    public int? MonthNumber { get; init; }
    public string? MonthTitle { get; init; }

    public int? WeekId { get; init; }
    public int? WeekNumber { get; init; }
    public string? WeekTitle { get; init; }

    public int? DayTopicId { get; init; }
    public int? DayNumber { get; init; }
    public string? DayTopicTitle { get; init; }
    public string? DayTopicDescription { get; init; }
    public int? EstimatedMinutes { get; init; }
}