namespace IGenServer.Domain.Entities;

public sealed class CourseSubject
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int SubjectId { get; set; }
    public int DisplayOrder { get; set; }
    public int StartMonth { get; set; }

    public Course Course { get; set; } = null!;
    public Subject Subject { get; set; } = null!;
}
