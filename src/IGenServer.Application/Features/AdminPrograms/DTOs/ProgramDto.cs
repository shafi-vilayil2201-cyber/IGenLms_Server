namespace IGenServer.Application.Features.AdminPrograms.DTOs;

public sealed record ProgramDto(
    int Id,
    string Name,
    string Code,
    bool IsActive);