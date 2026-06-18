using IGenServer.Application.Features.AdminPrograms.DTOs;
using MediatR;

namespace IGenServer.Application.Features.AdminPrograms.Queries.GetPrograms;

public sealed record GetProgramsQuery() : IRequest<IReadOnlyList<ProgramDto>>;