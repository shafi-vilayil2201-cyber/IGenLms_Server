namespace IGenServer.Application.Features.Auth.DTOs;

public sealed class AuthResponseDto
{
    public int UserId { get; init; }

    public string FullName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Role { get; init; } = string.Empty;

    public string AccessToken { get; init; } = string.Empty;

    public string NextStep { get; init; } = string.Empty;
}
