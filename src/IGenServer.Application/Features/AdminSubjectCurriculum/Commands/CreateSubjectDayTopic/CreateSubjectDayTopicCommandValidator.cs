using FluentValidation;

namespace IGenServer.Application.Features.AdminSubjectCurriculum.Commands.CreateSubjectDayTopic;

public sealed class CreateSubjectDayTopicCommandValidator : AbstractValidator<CreateSubjectDayTopicCommand>
{
    public CreateSubjectDayTopicCommandValidator()
    {
        RuleFor(x => x.SubjectWeekId).GreaterThan(0);
        RuleFor(x => x.DayNumber).GreaterThan(0).LessThanOrEqualTo(7);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.EstimatedMinutes).GreaterThan(0).LessThanOrEqualTo(600);
    }
}