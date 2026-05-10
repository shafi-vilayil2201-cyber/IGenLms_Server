using IGenServer.Domain.Enums;

namespace IGenServer.Domain.Entities;

public sealed class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public UserRole Role { get; set; }

    public StudentProfile? StudentProfile { get; set; }

    public MentorProfile? MentorProfile { get; set; }
}
