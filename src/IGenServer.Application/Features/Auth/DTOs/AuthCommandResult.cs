namespace IGenServer.Application.Features.Auth.DTOs;

public sealed class AuthCommandResult
{
    public AuthResponseDto Response { get; init; } = new();

    public string RefreshToken { get; init; } = string.Empty;
}
