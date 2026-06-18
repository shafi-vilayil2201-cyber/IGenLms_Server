namespace IGenServer.Domain.Entities;

public sealed class Subject
{
    public int Id { get; set; }
    public int ProgramId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int DurationMonths { get; set; }
    public bool IsPublished { get; set; }

    public Program Program { get; set; } = null!;
    public ICollection<SubjectMonth> Months { get; set; } = [];
    public ICollection<CourseSubject> CourseSubjects { get; set; } = [];
}
