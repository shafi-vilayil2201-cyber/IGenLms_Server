using FluentValidation;

namespace IGenServer.Application.Features.AdminPrograms.Commands.CreateProgram;

public sealed class CreateProgramCommandValidator : AbstractValidator<CreateProgramCommand>
{
    public CreateProgramCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(50)
            .Matches("^[a-zA-Z0-9-]+$")
            .WithMessage("Code can contain only letters, numbers, and hyphens.");
    }
}