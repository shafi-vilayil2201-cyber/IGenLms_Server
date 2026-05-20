using IGenServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IGenServer.Persistance.Data;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<StudentProfile> StudentProfiles => Set<StudentProfile>();
    public DbSet<MentorProfile> MentorProfiles => Set<MentorProfile>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<StudentCourseEnrollment> StudentCourseEnrollments => Set<StudentCourseEnrollment>();
    public DbSet<StudentHabit> StudentHabits => Set<StudentHabit>();
    public DbSet<StudentHabitLog> StudentHabitLogs => Set<StudentHabitLog>();
    public DbSet<StudentDailyScore> StudentDailyScores => Set<StudentDailyScore>();
    public DbSet<StudentFocusSession> StudentFocusSessions => Set<StudentFocusSession>();
    public DbSet<AutomationPromptLog> AutomationPromptLogs => Set<AutomationPromptLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
