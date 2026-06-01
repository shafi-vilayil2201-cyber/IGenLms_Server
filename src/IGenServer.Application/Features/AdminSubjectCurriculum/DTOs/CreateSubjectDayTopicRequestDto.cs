namespace IGenServer.Application.Features.AdminSubjectCurriculum.DTOs;

public sealed record CreateSubjectDayTopicRequestDto(
    int DayNumber,
    string Title,
    string Description,
    int EstimatedMinutes);