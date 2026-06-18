using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.AdminSubjectCurriculum.DTOs;
using IGenServer.Domain.Entities;
using MediatR;

namespace IGenServer.Application.Features.AdminSubjectCurriculum.Commands.CreateSubjectDayTopic;

public sealed class CreateSubjectDayTopicCommandHandler
    : IRequestHandler<CreateSubjectDayTopicCommand, SubjectDayTopicDto>
{
    private readonly ISubjectCurriculumRepository _repository;

    public CreateSubjectDayTopicCommandHandler(ISubjectCurriculumRepository repository)
    {
        _repository = repository;
    }

    public async Task<SubjectDayTopicDto> Handle(
        CreateSubjectDayTopicCommand command,
        CancellationToken cancellationToken)
    {
        var weekExists = await _repository.SubjectWeekIdExistsAsync(
            command.SubjectWeekId,
            cancellationToken);

        if (!weekExists)
        {
            throw new InvalidOperationException("Subject week does not exist.");
        }

        var dayTopicExists = await _repository.SubjectDayTopicExistsAsync(
            command.SubjectWeekId,
            command.DayNumber,
            cancellationToken);

        if (dayTopicExists)
        {
            throw new InvalidOperationException("Day topic already exists for this week.");
        }

        var dayTopic = new SubjectDayTopic
        {
            SubjectWeekId = command.SubjectWeekId,
            DayNumber = command.DayNumber,
            Title = command.Title.Trim(),
            Description = command.Description.Trim(),
            EstimatedMinutes = command.EstimatedMinutes
        };

        await _repository.AddDayTopicAsync(dayTopic, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new SubjectDayTopicDto(
            dayTopic.Id,
            dayTopic.SubjectWeekId,
            dayTopic.DayNumber,
            dayTopic.Title,
            dayTopic.Description,
            dayTopic.EstimatedMinutes);
    }
}