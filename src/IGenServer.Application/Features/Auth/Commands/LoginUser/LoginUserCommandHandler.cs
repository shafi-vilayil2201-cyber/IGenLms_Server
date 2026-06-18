
using IGenServer.Application.Abstractions.Authentication;
using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.Auth.DTOs;
using IGenServer.Application.Features.Auth.Enums;
using IGenServer.Domain.Entities;
using IGenServer.Domain.Enums;
using MediatR;

namespace IGenServer.Application.Features.Auth.Commands.LoginUser;

public sealed class LoginUserCommandHandler 
    : IRequestHandler<LoginUserCommand, AuthCommandResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenService _refreshTokenService;

    public LoginUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenService refreshTokenService
    )
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _refreshTokenService = refreshTokenService;
    }

    public async Task<AuthCommandResult> Handle(
        LoginUserCommand command,
        CancellationToken cancellationToken )
    {
        var user = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);

        if(user is null || !_passwordHasher.VerifyPassword(command.Password, user.PasswordHash))
        {
            throw new InvalidOperationException("Invalid email or password.");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);
        var refreshToken = _refreshTokenService.GenerateToken();

        user.RefreshTokenHash = _refreshTokenService.HashToken(refreshToken);
        user.RefreshTokenCreatedAtUtc = DateTime.UtcNow;
        user.RefreshTokenExpiresAtUtc = DateTime.UtcNow.AddDays(7);

        await _userRepository.SaveChangesAsync(cancellationToken);

        var nextStep = user.Role switch
        {
            UserRole.Student => RegistrationNextStep.StudentDashboard,
            UserRole.Mentor => RegistrationNextStep.MentorOnboarding,
            UserRole.Admin => RegistrationNextStep.AdminDashboard,
            _ => throw new UnauthorizedAccessException("Invalid email or password.")
        };

        return new AuthCommandResult
        {
            Response = new AuthResponseDto
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role.ToString(),
                AccessToken = token,
                NextStep = nextStep.ToString()
            },
            RefreshToken = refreshToken
        };
    }
}
