namespace IGenServer.Application.Features.Auth.DTOs;

public sealed class LoginUserRequestDto
{
    public string Email { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;
}
