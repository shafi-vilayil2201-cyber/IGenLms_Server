
using IGenServer.Application.Abstractions.Authentication;
using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.Auth.DTOs;
using IGenServer.Application.Features.Auth.Enums;
using IGenServer.Domain.Entities;
using IGenServer.Domain.Enums;
using MediatR;

namespace IGenServer.Application.Features.Auth.Commands.LoginUser;

public sealed class LoginUserCommandHandler 
    : IRequestHandler<LoginUserCommand, AuthResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator
    )
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }

    public async Task<AuthResponseDto> Handle(
        LoginUserCommand command,
        CancellationToken cancellationToken )
    {
        var user = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);

        if(user is null || !_passwordHasher.VerifyPassword(command.Password, user.PasswordHash))
        {
            throw new InvalidOperationException("Invalid email or password.");
        }

        var passwordIsValid = _passwordHasher.VerifyPassword(command.Password, user.PasswordHash);

        if(!passwordIsValid)
        {
            throw new InvalidOperationException("Invalid email or password.");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        var NextStep = user.Role switch
        {
            UserRole.Student => RegistrationNextStep.StudentDashboard,
            UserRole.Mentor => RegistrationNextStep.MentorOnboarding,
            UserRole.Admin => RegistrationNextStep.AdminDashboard,
            _ => throw new InvalidOperationException("Unsupported user role.")
        };

        return new AuthResponseDto
        {
          UserId = user.Id,
          FullName = user.FullName,
          Email = user.Email,
          Role = user.Role.ToString(),
          Token =   token,
          NextStep = NextStep.ToString()

        };
    }
}