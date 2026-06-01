namespace IGenServer.Domain.Entities;

public sealed class SubjectWeek
{
    public int Id { get; set; }
    public int SubjectMonthId { get; set; }
    public int WeekNumber { get; set; }
    public string Title { get; set; } = string.Empty;

    public SubjectMonth SubjectMonth { get; set; } = null!;
    public ICollection<SubjectDayTopic> DayTopics { get; set; } = [];
}
