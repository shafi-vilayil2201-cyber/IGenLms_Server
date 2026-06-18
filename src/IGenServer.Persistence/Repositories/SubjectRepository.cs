using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Domain.Entities;
using IGenServer.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace IGenServer.Persistence.Repositories;

public sealed class SubjectRepository : ISubjectRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SubjectRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Subject subject, CancellationToken cancellationToken = default)
    {
        await _dbContext.Subjects.AddAsync(subject, cancellationToken);
    }

    public Task<bool> ProgramExistsAsync(int programId, CancellationToken cancellationToken = default)
    {
        return _dbContext.Programs.AnyAsync(x => x.Id == programId, cancellationToken);
    }

    public Task<bool> SubjectExistsAsync(int programId, string name, CancellationToken cancellationToken = default)
    {
        return _dbContext.Subjects.AnyAsync(
            x => x.ProgramId == programId && x.Name == name,
            cancellationToken);
    }

    public async Task<IReadOnlyList<Subject>> GetByProgramIdAsync(
        int programId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Subjects
            .Where(x => x.ProgramId == programId)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}