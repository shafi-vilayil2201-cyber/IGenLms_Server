namespace IGenServer.Application.Features.AdminSubjectCurriculum.DTOs;

public sealed record SubjectCurriculumDayTopicDto(
    int Id,
    int DayNumber,
    string Title,
    string Description,
    int EstimatedMinutes);