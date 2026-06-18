
namespace IGenServer.Application.Features.Auth.DTOs;

public sealed class RefreshTokenRequestDto
{
    public string RefreshToken {get;set;} = string.Empty;
}