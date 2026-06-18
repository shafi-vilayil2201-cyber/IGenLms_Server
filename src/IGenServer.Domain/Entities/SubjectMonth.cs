namespace IGenServer.Domain.Entities;

public sealed class SubjectMonth
{
    public int Id { get; set; }
    public int SubjectId { get; set; }
    public int MonthNumber { get; set; }
    public string Title { get; set; } = string.Empty;

    public Subject Subject { get; set; } = null!;
    public ICollection<SubjectWeek> Weeks { get; set; } = [];
}
