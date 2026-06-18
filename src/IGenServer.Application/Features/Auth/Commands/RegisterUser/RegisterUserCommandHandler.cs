using System.Text.Json;
using IGenServer.Application.Abstractions.Authentication;
using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.Auth.DTOs;
using IGenServer.Application.Features.Auth.Enums;
using IGenServer.Domain.Entities;
using IGenServer.Domain.Enums;
using MediatR;

namespace IGenServer.Application.Features.Auth.Commands.RegisterUser;

public sealed class RegisterUserCommandHandler
    : IRequestHandler<RegisterUserCommand, AuthCommandResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenService _refreshTokenService;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenService refreshTokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenService = refreshTokenService;
    }

    public async Task<AuthCommandResult> Handle(
        RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        var roleParsed = Enum.TryParse<UserRole>(command.Role, true, out var userRole);

        if (!roleParsed)
        {
            throw new InvalidOperationException("Invalid user role.");
        }

        if (userRole == UserRole.Admin)
        {
            throw new InvalidOperationException("Admin cannot register from the public registration flow.");
        }

        var emailExists = await _userRepository.EmailExistsAsync(command.Email, cancellationToken);

        if (emailExists)
        {
            throw new InvalidOperationException("A user with this email already exists.");
        }

        var user = new User
        {
            FullName = command.FullName,
            Email = command.Email,
            PasswordHash = _passwordHasher.HashPassword(command.Password),
            Role = userRole
        };

        RegistrationNextStep nextStep;

        if (userRole == UserRole.Student)
        {
            user.StudentProfile = new StudentProfile
            {
                TargetYear = command.TargetYear!.Value,
                StudyStreak = 0,
                Rank = 0
            };

            nextStep = RegistrationNextStep.StudentDashboard;
        }
        else
        {
            user.MentorProfile = new MentorProfile
            {
                ExpertiseJson = JsonSerializer.Serialize(command.Expertise!),
                ApprovalStatus = MentorApprovalStatus.Pending
            };

            nextStep = RegistrationNextStep.MentorOnboarding;
        }

        await _userRepository.AddAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);

        var token = _jwtTokenGenerator.GenerateToken(user);

        var refreshToken = _refreshTokenService.GenerateToken();

        user.RefreshTokenHash = _refreshTokenService.HashToken(refreshToken);
        user.RefreshTokenCreatedAtUtc = DateTime.UtcNow;
        user.RefreshTokenExpiresAtUtc = DateTime.UtcNow.AddDays(7);

        await _userRepository.SaveChangesAsync(cancellationToken);

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
