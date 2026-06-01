using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.AdminSubjectCurriculum.DTOs;
using IGenServer.Domain.Entities;
using MediatR;

namespace IGenServer.Application.Features.AdminSubjectCurriculum.Commands.CreateSubjectMonth;

public sealed class CreateSubjectMonthCommandHandler
    : IRequestHandler<CreateSubjectMonthCommand, SubjectMonthDto>
{
    private readonly ISubjectCurriculumRepository _repository;

    public CreateSubjectMonthCommandHandler(ISubjectCurriculumRepository repository)
    {
        _repository = repository;
    }

    public async Task<SubjectMonthDto> Handle(
        CreateSubjectMonthCommand command,
        CancellationToken cancellationToken)
    {
        var subjectExists = await _repository.SubjectExistsAsync(command.SubjectId, cancellationToken);
        if (!subjectExists)
        {
            throw new InvalidOperationException("Subject does not exist.");
        }

        var monthExists = await _repository.SubjectMonthExistsAsync(
            command.SubjectId,
            command.MonthNumber,
            cancellationToken);

        if (monthExists)
        {
            throw new InvalidOperationException("Month already exists for this subject.");
        }

        var month = new SubjectMonth
        {
            SubjectId = command.SubjectId,
            MonthNumber = command.MonthNumber,
            Title = command.Title.Trim()
        };

        await _repository.AddMonthAsync(month, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new SubjectMonthDto(
            month.Id,
            month.SubjectId,
            month.MonthNumber,
            month.Title);
    }
}