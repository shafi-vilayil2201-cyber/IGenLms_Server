

using System.Security.Cryptography;
using IGenServer.Application.Abstractions.Authentication;
using System.Text;

namespace IGenServer.Infrastructure.Authentication;

public sealed class RefreshTokenService : IRefreshTokenService
{
    public string GenerateToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(randomBytes);
    }
    public string HashToken(string token)
    {
        var bytes = Encoding.UTF8.GetBytes(token);
        var hash = SHA256.HashData(bytes);

        return Convert.ToBase64String(hash);

    }
    public bool VerifyToken(string token, string tokenHash)
    {
        var hashedToken = HashToken(token);
        return hashedToken == tokenHash;
    }
}