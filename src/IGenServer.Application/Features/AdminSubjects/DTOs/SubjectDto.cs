namespace IGenServer.Application.Features.AdminSubjects.DTOs;

public sealed record SubjectDto(
    int Id,
    int ProgramId,
    string Name,
    string Description,
    int DurationMonths,
    bool IsPublished);