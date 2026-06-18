using IGenServer.Application.Features.AdminPrograms.DTOs;
using MediatR;

namespace IGenServer.Application.Features.AdminPrograms.Commands.CreateProgram;

public sealed record CreateProgramCommand(
    string Name,
    string Code) : IRequest<ProgramDto>;