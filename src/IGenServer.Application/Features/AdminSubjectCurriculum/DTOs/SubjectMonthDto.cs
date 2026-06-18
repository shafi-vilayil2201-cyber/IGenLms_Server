namespace IGenServer.Application.Features.AdminSubjectCurriculum.DTOs;

public sealed record SubjectMonthDto(
    int Id,
    int SubjectId,
    int MonthNumber,
    string Title);