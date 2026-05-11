
using FluentValidation;
using IGenServer.Domain.Entities;
using IGenServer.Domain.Enums;

namespace IGenServer.Application.Features.Auth.Commands.RegisterUser;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x=> x.FullName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(250);
        
        RuleFor(x=> x.Password)
            .NotEmpty()
            .MinimumLength(8);
        RuleFor(x => x.Role)
            .NotEmpty()
            .Must(BeAValidPublicRiole)
            .WithMessage("Role must be Student or Mentor");

        When(x => x.Role.Equals(UserRole.Student.ToString(),StringComparison.OrdinalIgnoreCase),()=>
        {
            RuleFor(x => x.TargetYear)
                .NotNull()
                .GreaterThan(0);
        });

        When(x => x.Role.Equals(UserRole.Mentor.ToString(),StringComparison.OrdinalIgnoreCase),()=>
        {
            RuleFor(x => x.Expertise)
                .NotNull()
                .Must(x => x is {Count: > 0})
                .WithMessage("At least one expertise is required for mentor registration");
        });
    }
    private static bool BeAValidPublicRiole(string role)
    {
        return role.Equals(UserRole.Student.ToString(),StringComparison.OrdinalIgnoreCase)
            || role.Equals(UserRole.Mentor.ToString(),StringComparison.OrdinalIgnoreCase);
    }
}