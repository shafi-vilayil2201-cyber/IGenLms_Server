using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.AdminSubjectCurriculum.DTOs;
using IGenServer.Domain.Entities;
using MediatR;

namespace IGenServer.Application.Features.AdminSubjectCurriculum.Commands.CreateSubjectWeek;

public sealed class CreateSubjectWeekCommandHandler
    : IRequestHandler<CreateSubjectWeekCommand, SubjectWeekDto>
{
    private readonly ISubjectCurriculumRepository _repository;

    public CreateSubjectWeekCommandHandler(ISubjectCurriculumRepository repository)
    {
        _repository = repository;
    }

    public async Task<SubjectWeekDto> Handle(
        CreateSubjectWeekCommand command,
        CancellationToken cancellationToken)
    {
        var monthExists = await _repository.SubjectMonthIdExistsAsync(
            command.SubjectMonthId,
            cancellationToken);

        if (!monthExists)
        {
            throw new InvalidOperationException("Subject month does not exist.");
        }

        var weekExists = await _repository.SubjectWeekExistsAsync(
            command.SubjectMonthId,
            command.WeekNumber,
            cancellationToken);

        if (weekExists)
        {
            throw new InvalidOperationException("Week already exists for this month.");
        }

        var week = new SubjectWeek
        {
            SubjectMonthId = command.SubjectMonthId,
            WeekNumber = command.WeekNumber,
            Title = command.Title.Trim()
        };

        await _repository.AddWeekAsync(week, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new SubjectWeekDto(
            week.Id,
            week.SubjectMonthId,
            week.WeekNumber,
            week.Title);
    }
}