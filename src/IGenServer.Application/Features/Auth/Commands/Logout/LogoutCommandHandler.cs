using IGenServer.Application.Abstractions.Authentication;
using IGenServer.Application.Abstractions.Persistence;
using MediatR;

namespace IGenServer.Application.Features.Auth.Commands.Logout;

public sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenService _refreshTokenService;

    public LogoutCommandHandler(
        IUserRepository userRepository,
        IRefreshTokenService refreshTokenService)
    {
        _userRepository = userRepository;
        _refreshTokenService = refreshTokenService;
    }

    public async Task Handle(
    LogoutCommand request,
    CancellationToken cancellationToken)
    {
        var refreshTokenHash = _refreshTokenService.HashToken(request.RefreshToken);

        var user = await _userRepository.GetByRefreshTokenHashAsync(
            refreshTokenHash,
            cancellationToken);

        if (user is null)
        {
            return;
        }

        user.RefreshTokenHash = null;
        user.RefreshTokenCreatedAtUtc = null;
        user.RefreshTokenExpiresAtUtc = null;

        await _userRepository.SaveChangesAsync(cancellationToken);
    }

}
