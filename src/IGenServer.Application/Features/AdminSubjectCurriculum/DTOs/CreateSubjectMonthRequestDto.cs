namespace IGenServer.Application.Features.AdminSubjectCurriculum.DTOs;

public sealed record CreateSubjectMonthRequestDto(
    int MonthNumber,
    string Title);