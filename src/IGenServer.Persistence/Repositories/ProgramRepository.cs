

using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Domain.Entities;
using IGenServer.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace IGenServer.Persistance.Repositories;

public sealed class ProgramRepository : IProgramRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProgramRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(Program program,CancellationToken cancellationToken= default)
    {
        await _dbContext.Programs.AddAsync(program,cancellationToken);
    }
    public Task<bool> CodeExistsAsync(string code, CancellationToken cancellationToken)
    {
        return _dbContext.Programs.AnyAsync(x=> x.Code == code, cancellationToken);
    }
    public async Task<IReadOnlyList<Program>> GetAllAsync(CancellationToken cancellationToken=default)
    {
        return await _dbContext.Programs
            .OrderBy(x=> x.Name)
            .ToListAsync(cancellationToken);
    }
    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
} 