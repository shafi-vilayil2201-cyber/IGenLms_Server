using IGenServer.Domain.Enums;

namespace IGenServer.Domain.Entities;

public sealed class MentorProfile
{
    public int UserId { get; set; }

    public string ExpertiseJson { get; set; } = "[]";

    public MentorApprovalStatus ApprovalStatus { get; set; }

    public User User { get; set; } = null!;
}