namespace IGenServer.Application.Features.AdminSubjectCurriculum.DTOs;

public sealed record SubjectDayTopicDto(
    int Id,
    int SubjectWeekId,
    int DayNumber,
    string Title,
    string Description,
    int EstimatedMinutes);