using FluentValidation;

namespace IGenServer.Application.Features.AdminSubjectCurriculum.Commands.CreateSubjectWeek;

public sealed class CreateSubjectWeekCommandValidator : AbstractValidator<CreateSubjectWeekCommand>
{
    public CreateSubjectWeekCommandValidator()
    {
        RuleFor(x => x.SubjectMonthId).GreaterThan(0);
        RuleFor(x => x.WeekNumber).GreaterThan(0).LessThanOrEqualTo(6);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(150);
    }
}