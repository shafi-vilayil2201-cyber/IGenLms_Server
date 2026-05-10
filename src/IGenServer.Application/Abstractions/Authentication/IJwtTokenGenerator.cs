using IGenServer.Domain.Entities;

namespace IGenServer.Application.Abstractions.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
