namespace IGenServer.Application.Features.Auth.DTOs;

public sealed class RegisterUserRequestDto
{
    public string FullName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public string Role { get; init; } = string.Empty;

    public int? TargetYear { get; init; }

    public List<string>? Expertise { get; init; }
}
