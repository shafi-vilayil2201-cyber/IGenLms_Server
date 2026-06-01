namespace IGenServer.Domain.Entities;

public sealed class Program
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public ICollection<Subject> Subjects { get; set; } = [];
    public ICollection<Course> Courses { get; set; } = [];
}
