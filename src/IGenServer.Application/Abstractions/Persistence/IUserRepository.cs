using IGenServer.Domain.Entities;

namespace IGenServer.Application.Abstractions.Persistence;

public interface IUserRepository
{
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);

    Task AddAsync(User user, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<User?> GetByEmailAsync(string email,CancellationToken cancellationToken = default);
    Task<User?> GetByRefreshTokenHashAsync(string RefreshTokenHash,CancellationToken cancellationToken);

}
