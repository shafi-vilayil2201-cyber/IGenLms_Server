namespace IGenServer.Domain.Entities;

public sealed class StudentCourseEnrollment
{
    public int Id { get; set; }

    public int StudentUserId { get; set; }

    public int CourseId { get; set; }

    public DateTime EnrolledAtUtc { get; set; }

    public User Student { get; set; } = null!;

    public Course Course { get; set; } = null!;
}
