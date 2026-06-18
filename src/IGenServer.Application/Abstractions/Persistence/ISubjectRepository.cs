using IGenServer.Domain.Entities;

namespace IGenServer.Application.Abstractions.Persistence;
public interface ISubjectRepository
{
    Task AddAsync(Subject subject, CancellationToken cancellationToken);
    Task<bool> ProgramExistsAsync(int programId, CancellationToken cancellationToken = default);
    Task<bool> SubjectExistsAsync(int programId,string name, CancellationToken cancellationToken= default);
    Task<IReadOnlyList<Subject>> GetByProgramIdAsync(int programId,CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}