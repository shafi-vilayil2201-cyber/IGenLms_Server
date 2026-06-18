using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.AdminPrograms.DTOs;
using IGenServer.Domain.Entities;
using MediatR;

namespace IGenServer.Application.Features.AdminPrograms.Commands.CreateProgram;

public sealed class CreateProgramCommandHandler : IRequestHandler<CreateProgramCommand, ProgramDto>
{
    private readonly IProgramRepository _programRepository;

    public CreateProgramCommandHandler(IProgramRepository programRepository)
    {
        _programRepository = programRepository;
    }

    public async Task<ProgramDto> Handle(CreateProgramCommand command, CancellationToken cancellationToken)
    {
        var normalizedCode = command.Code.Trim().ToLowerInvariant();

        var exists = await _programRepository.CodeExistsAsync(normalizedCode, cancellationToken);
        if (exists)
        {
            throw new InvalidOperationException("Program code already exists.");
        }

        var program = new Program
        {
            Name = command.Name.Trim(),
            Code = normalizedCode,
            IsActive = true
        };

        await _programRepository.AddAsync(program, cancellationToken);
        await _programRepository.SaveChangesAsync(cancellationToken);

        return new ProgramDto(
            program.Id,
            program.Name,
            program.Code,
            program.IsActive);
    }
}