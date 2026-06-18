using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.AdminPrograms.DTOs;
using MediatR;

namespace IGenServer.Application.Features.AdminPrograms.Queries.GetPrograms;

public sealed class GetProgramsQueryHandler : IRequestHandler<GetProgramsQuery, IReadOnlyList<ProgramDto>>
{
    private readonly IProgramRepository _programRepository;

    public GetProgramsQueryHandler(IProgramRepository programRepository)
    {
        _programRepository = programRepository;
    }

    public async Task<IReadOnlyList<ProgramDto>> Handle(GetProgramsQuery query, CancellationToken cancellationToken)
    {
        var programs = await _programRepository.GetAllAsync(cancellationToken);

        return programs
            .Select(program => new ProgramDto(
                program.Id,
                program.Name,
                program.Code,
                program.IsActive))
            .ToList();
    }
}