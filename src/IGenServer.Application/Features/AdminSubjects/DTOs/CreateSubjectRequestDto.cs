namespace IGenServer.Application.Features.AdminSubjects.DTOs;

public sealed record CreateSubjectRequestDto(
    int ProgramId,
    string Name,
    string Description,
    int DurationMonths);