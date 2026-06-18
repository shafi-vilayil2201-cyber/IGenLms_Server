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
    public DbSet<Program> Programs => Set<Program>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<SubjectMonth> SubjectMonths => Set<SubjectMonth>();
    public DbSet<SubjectWeek> SubjectWeeks => Set<SubjectWeek>();
    public DbSet<SubjectDayTopic> SubjectDayTopics => Set<SubjectDayTopic>();
    public DbSet<CourseSubject> CourseSubjects => Set<CourseSubject>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
