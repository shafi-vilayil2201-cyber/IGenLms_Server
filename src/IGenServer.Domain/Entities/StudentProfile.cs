namespace IGenServer.Domain.Entities;

public sealed class StudentProfile
{
    public int UserId { get; set; }

    public int TargetYear { get; set; }

    public int StudyStreak { get; set; }

    public int Rank { get; set; }

    public User User { get; set; } = null!;
}