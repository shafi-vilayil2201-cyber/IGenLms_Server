namespace IGenServer.Domain.Entities;

public sealed class SubjectDayTopic
{
    public int Id { get; set; }
    public int SubjectWeekId { get; set; }
    public int DayNumber { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int EstimatedMinutes { get; set; }

    public SubjectWeek SubjectWeek { get; set; } = null!;
}
