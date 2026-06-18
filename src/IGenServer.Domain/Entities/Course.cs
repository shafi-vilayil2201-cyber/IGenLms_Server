namespace IGenServer.Domain.Entities;

public sealed class Course
{
    public int Id { get; set; }
    public int ProgramId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int DurationMonths { get; set; }
    public decimal Price { get; set; }
    public string Status { get; set; } = "Draft";

    public Program Program { get; set; } = null!;
    public ICollection<CourseSubject> CourseSubjects { get; set; } = [];
    public ICollection<StudentCourseEnrollment> Enrollments { get; set; } = [];
}
