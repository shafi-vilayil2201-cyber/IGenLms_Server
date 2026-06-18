
using FluentValidation;

namespace IGenServer.Application.Features.AdminSubjects.Commands.CreateSubject;

public sealed class CreateSubjectCommandValidator : AbstractValidator<CreateSubjectCommand>
{
    public CreateSubjectCommandValidator()
    {
        RuleFor(x => x.ProgramId)
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(1000);
        
        RuleFor(x => x.DurationMonths)
            .GreaterThan(0)
            .LessThanOrEqualTo(24);
    }
}