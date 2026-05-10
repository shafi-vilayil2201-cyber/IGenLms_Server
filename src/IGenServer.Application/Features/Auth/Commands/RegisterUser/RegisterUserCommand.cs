namespace IGenServer.Application.Features.Auth.Commands.RegisterUser;

public sealed record RegisterUserCommand(
    string FullName,
    string Email,
    string Password,
    string Role,
    int? TargetYear,
    List<string>? Expertise);
