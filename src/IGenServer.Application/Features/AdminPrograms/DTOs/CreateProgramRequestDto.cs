namespace IGenServer.Application.Features.AdminPrograms.DTOs;

public sealed record CreateProgramRequestDto(
    string Name,
    string Code);