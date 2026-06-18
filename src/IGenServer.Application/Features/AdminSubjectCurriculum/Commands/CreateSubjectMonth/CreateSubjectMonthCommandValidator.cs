using FluentValidation;

namespace IGenServer.Application.Features.AdminSubjectCurriculum.Commands.CreateSubjectMonth;

public sealed class CreateSubjectMonthCommandValidator : AbstractValidator<CreateSubjectMonthCommand>
{
    public CreateSubjectMonthCommandValidator()
    {
        RuleFor(x => x.SubjectId).GreaterThan(0);
        RuleFor(x => x.MonthNumber).GreaterThan(0).LessThanOrEqualTo(24);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(150);
    }
}