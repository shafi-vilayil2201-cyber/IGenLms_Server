

using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.AdminSubjects.DTOs;
using IGenServer.Domain.Entities;
using MediatR;

namespace IGenServer.Application.Features.AdminSubjects.Commands.CreateSubject;

public sealed class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, SubjectDto>
{
    private readonly ISubjectRepository _subjectRepository;

    public CreateSubjectCommandHandler(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }
    public async Task<SubjectDto> Handle(CreateSubjectCommand command, CancellationToken cancellationToken)
    {
        var programExists = await _subjectRepository.ProgramExistsAsync(command.ProgramId,cancellationToken);
        if(!programExists)
        {
            throw new InvalidOperationException("Program does not exist.");
        }

        var normalizedName = command.Name.Trim();

        var subjectExists = await _subjectRepository.SubjectExistsAsync(
            command.ProgramId,
            normalizedName,
            cancellationToken
        );
        
        if(subjectExists)
        {
            throw new InvalidOperationException("Subject already exists in this program.");
        }

        var subject = new Subject
        {
            ProgramId = command.ProgramId,
            Name = normalizedName,
            Description = command.Description.Trim(),
            DurationMonths = command.DurationMonths,
            IsPublished = false
        };

        await _subjectRepository.AddAsync(subject, cancellationToken);
        await _subjectRepository.SaveChangesAsync(cancellationToken);

        return new SubjectDto(
            subject.Id,
            subject.ProgramId,
            subject.Name,
            subject.Description,
            subject.DurationMonths,
            subject.IsPublished
        );
    }
}