namespace IGenServer.Application.Features.AdminSubjectCurriculum.DTOs;

public sealed record SubjectWeekDto(
    int Id,
    int SubjectMonthId,
    int WeekNumber,
    string Title);