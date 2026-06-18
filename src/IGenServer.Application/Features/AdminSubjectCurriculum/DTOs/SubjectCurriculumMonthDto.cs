namespace IGenServer.Application.Features.AdminSubjectCurriculum.DTOs;

public sealed record SubjectCurriculumMonthDto(
    int Id,
    int MonthNumber,
    string Title,
    IReadOnlyList<SubjectCurriculumWeekDto> Weeks);