using IGenServer.Application.Abstractions.Authentication;
using IGenServer.Domain.Entities;
using IGenServer.Domain.Enums;
using IGenServer.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace IGenServer.API.Seeding;

public static class DemoDataSeeder
{
    public static async Task SeedDevelopmentDataAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

        await dbContext.Database.MigrateAsync();

        var demoAccounts = new DemoAccountSeed[]
        {
            new()
            {
                Email = "student@upsc.com",
                FullName = "Demo Student",
                Password = "password123",
                Role = UserRole.Student,
                StudentProfile = new StudentProfile
                {
                    TargetYear = 2027,
                    StudyStreak = 12,
                    Rank = 128
                }
            },
            new()
            {
                Email = "mentor@upsc.com",
                FullName = "Demo Mentor",
                Password = "password123",
                Role = UserRole.Mentor,
                MentorProfile = new MentorProfile
                {
                    ExpertiseJson = "[\"Polity\",\"Ethics\",\"Essay\"]",
                    ApprovalStatus = MentorApprovalStatus.Approved
                }
            },
            new()
            {
                Email = "admin@igen.com",
                FullName = "Demo Admin",
                Password = "admin123",
                Role = UserRole.Admin
            }
        };

        foreach (var account in demoAccounts)
        {
            var existingUser = await dbContext.Users
                .Include(x => x.StudentProfile)
                .Include(x => x.MentorProfile)
                .FirstOrDefaultAsync(x => x.Email == account.Email);

            if (existingUser is not null)
            {
                continue;
            }

            var user = new User
            {
                FullName = account.FullName,
                Email = account.Email,
                PasswordHash = passwordHasher.HashPassword(account.Password),
                Role = account.Role
            };

            if (account.Role == UserRole.Student)
            {
                user.StudentProfile = account.StudentProfile;
            }
            else if (account.Role == UserRole.Mentor)
            {
                user.MentorProfile = account.MentorProfile;
            }

            dbContext.Users.Add(user);
        }

        await dbContext.SaveChangesAsync();
    }
}

file sealed class DemoAccountSeed
{
    public required string Email { get; init; }
    public required string FullName { get; init; }
    public required string Password { get; init; }
    public required UserRole Role { get; init; }
    public StudentProfile? StudentProfile { get; init; }
    public MentorProfile? MentorProfile { get; init; }
}
