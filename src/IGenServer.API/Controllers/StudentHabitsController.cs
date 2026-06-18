using System.Security.Claims;
using IGenServer.Application.Common.Responses;
using IGenServer.Application.Features.StudentHabits.Commands.CreateStudentHabit;
using IGenServer.Application.Features.StudentHabits.DTOs;
using IGenServer.Application.Features.StudentHabits.Queries.GetTodayStudentHabits;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IGenServer.API.Controllers;

[ApiController]
[Route("api/student/habits")]
[Authorize]
public sealed class StudentHabitsController : ControllerBase
{
    private readonly ISender _sender;

    public StudentHabitsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<CommonResponse<StudentHabitDto>>> Create(
        CreateStudentHabitRequestDto request,
        CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();

        var result = await _sender.Send(
            new CreateStudentHabitCommand(
                userId,
                request.Title,
                request.Description,
                request.HabitType,
                request.ReminderTime,
                request.IsReminderEnabled,
                request.TargetMinutes,
                request.Points),
            cancellationToken);

        return Ok(CommonResponse<StudentHabitDto>.SuccessResponse(
            result,
            "Habit created successfully."));
    }
    [HttpGet("today")]
    public async Task<ActionResult<CommonResponse<IReadOnlyList<StudentHabitDto>>>> GetToday(
        CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();

        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var result = await _sender.Send(
            new GetTodayStudentHabitsQuery(userId, today),
            cancellationToken);

        return Ok(CommonResponse<IReadOnlyList<StudentHabitDto>>.SuccessResponse(
            result,
            "Today's habits loaded successfully."));
    }

    private int GetCurrentUserId()
    {
        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdValue, out var userId))
        {
            throw new UnauthorizedAccessException("Invalid user token.");
        }

        return userId;
    }
}