

using IGenServer.Application.Features.Auth.DTOs;
using MediatR;

namespace IGenServer.Application.Features.Auth.Commands.RefreshToken;

public sealed record RefreshTokenCommand(
    string RefreshToken) : IRequest<AuthCommandResult>;
