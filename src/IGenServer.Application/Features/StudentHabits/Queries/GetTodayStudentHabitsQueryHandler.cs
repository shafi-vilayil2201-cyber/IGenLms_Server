using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.StudentHabits.DTOs;
using MediatR;

namespace IGenServer.Application.Features.StudentHabits.Queries.GetTodayStudentHabits;

public sealed class GetTodayStudentHabitsQueryHandler
    : IRequestHandler<GetTodayStudentHabitsQuery, IReadOnlyList<StudentHabitDto>>
{
    private readonly IStudentHabitRepository _studentHabitRepository;

    public GetTodayStudentHabitsQueryHandler(IStudentHabitRepository studentHabitRepository)
    {
        _studentHabitRepository = studentHabitRepository;
    }

    public async Task<IReadOnlyList<StudentHabitDto>> Handle(
        GetTodayStudentHabitsQuery query,
        CancellationToken cancellationToken)
    {
        return await _studentHabitRepository.GetTodayHabitsAsync(
            query.UserId,
            query.Date,
            cancellationToken);
    }
}