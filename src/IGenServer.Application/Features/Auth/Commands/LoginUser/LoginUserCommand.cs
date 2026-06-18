using IGenServer.Application.Features.Auth.DTOs;
using MediatR;

namespace IGenServer.Application.Features.Auth.Commands.LoginUser;

public sealed record LoginUserCommand(
    string Email,
    string Password
    ) :IRequest<AuthCommandResult>;
