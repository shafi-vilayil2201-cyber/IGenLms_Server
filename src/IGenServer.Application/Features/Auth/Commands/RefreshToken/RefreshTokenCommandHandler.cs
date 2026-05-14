

using IGenServer.Application.Abstractions.Authentication;
using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.Auth.DTOs;
using IGenServer.Application.Features.Auth.Enums;
using IGenServer.Domain.Enums;
using MediatR;

namespace IGenServer.Application.Features.Auth.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler
    : IRequestHandler<RefreshTokenCommand,AuthCommandResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenService _refreshTokenService;

    public RefreshTokenCommandHandler(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenService refreshTokenService
    )
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenService = refreshTokenService;
        _userRepository = userRepository;
    }

    public async Task<AuthCommandResult> Handle(
        RefreshTokenCommand command,
        CancellationToken cancellationToken
    )
    {
       var refreshTokenHash = _refreshTokenService.HashToken(command.RefreshToken);

       var user = await _userRepository.GetByRefreshTokenHashAsync(
        refreshTokenHash , cancellationToken
       );


        if (user is null)
        {
            throw new UnauthorizedAccessException("Invalid refresh token.");
        }

       if(user.RefreshTokenExpiresAtUtc is null || user.RefreshTokenExpiresAtUtc <= DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Refresh token expired.");
        }

        var accessToken = _jwtTokenGenerator.GenerateToken(user);
        var newRefreshToken = _refreshTokenService.GenerateToken();

        user.RefreshTokenHash = _refreshTokenService.HashToken(newRefreshToken);
        user.RefreshTokenCreatedAtUtc = DateTime.UtcNow;
        user.RefreshTokenExpiresAtUtc = DateTime.UtcNow.AddDays(7);

        await _userRepository.SaveChangesAsync(cancellationToken);

        var nextStep = user.Role switch
        {
            UserRole.Student => RegistrationNextStep.StudentDashboard,
            UserRole.Mentor => RegistrationNextStep.MentorOnboarding,
            UserRole.Admin => RegistrationNextStep.AdminDashboard,
            _ => throw new InvalidOperationException("Unsupported user role.")
        };

        return new AuthCommandResult
        {
            Response = new AuthResponseDto
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role.ToString(),
                AccessToken = accessToken,
                NextStep = nextStep.ToString()
            },
             RefreshToken = newRefreshToken
        };
    }

}
