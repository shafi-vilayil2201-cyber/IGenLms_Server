

using FluentValidation;

namespace IGenServer.Application.Features.Auth.Commands.LoginUser;

public sealed class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256);
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);
    }
}