namespace IGenServer.Application.Features.AdminSubjectCurriculum.DTOs;

public sealed record CreateSubjectWeekRequestDto(
    int WeekNumber,
    string Title);