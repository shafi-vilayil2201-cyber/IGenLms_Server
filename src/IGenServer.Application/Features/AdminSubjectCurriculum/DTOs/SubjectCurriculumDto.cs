namespace IGenServer.Application.Features.AdminSubjectCurriculum.DTOs;

public sealed record SubjectCurriculumDto(
    int SubjectId,
    string SubjectName,
    int ProgramId,
    IReadOnlyList<SubjectCurriculumMonthDto> Months);