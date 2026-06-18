using IGenServer.Domain.Entities;

namespace IGenServer.Application.Abstractions.Persistence;

public interface IProgramRepository
{
    Task AddAsync(Program program, CancellationToken cancellationToken = default);
    Task<bool> CodeExistsAsync(string code, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Program>> GetAllAsync(CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}