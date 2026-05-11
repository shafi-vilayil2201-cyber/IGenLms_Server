
using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Domain.Entities;
using IGenServer.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace IGenServer.Persistance.Repositories;

public sealed class UserRepository :IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task<bool> EmailExistsAsync(string email,CancellationToken cancellationToken = default)
    {
        return _dbContext.Users.AnyAsync(x => x.Email ==email,cancellationToken);
    }
    public async Task AddAsync(User user,CancellationToken cancellationToken = default)
    {
        await _dbContext.Users.AddAsync(user,cancellationToken);
    }
    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return _dbContext.Users
            .Include(x => x.StudentProfile)
            .Include(x => x.MentorProfile)
            .FirstOrDefaultAsync(x => x.Email == email,cancellationToken);
    }
}