using System.Security.Claims;
using IGenServer.Application.Common.Responses;
using IGenServer.Application.Features.StudentCourses.Commands.EnrollInCourse;
using IGenServer.Application.Features.StudentCourses.DTOs;
using IGenServer.Application.Features.StudentCourses.Queries.GetAvailableCourses;
using IGenServer.Application.Features.StudentCourses.Queries.GetMyCourses;
using IGenServer.Application.Features.StudentCourses.Queries.GetStudentCourseDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IGenServer.API.Controllers;

[ApiController]
[Route("api/student/courses")]
[Authorize(Roles = "Student")]
public sealed class StudentCoursesController : ControllerBase
{
    private readonly ISender _sender;

    public StudentCoursesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult<CommonResponse<IReadOnlyList<StudentCourseDto>>>> GetAvailable(
        CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();

        var result = await _sender.Send(
            new GetAvailableCoursesQuery(userId),
            cancellationToken);

        return Ok(CommonResponse<IReadOnlyList<StudentCourseDto>>.SuccessResponse(
            result,
            "Available courses loaded successfully."));
    }

    [HttpGet("my")]
    public async Task<ActionResult<CommonResponse<IReadOnlyList<StudentCourseDto>>>> GetMyCourses(
        CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();

        var result = await _sender.Send(
            new GetMyCoursesQuery(userId),
            cancellationToken);

        return Ok(CommonResponse<IReadOnlyList<StudentCourseDto>>.SuccessResponse(
            result,
            "Enrolled courses loaded successfully."));
    }

    [HttpGet("{courseId:int}")]
    public async Task<ActionResult<CommonResponse<StudentCourseDetailsDto>>> GetDetails(
        int courseId,
        CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();

        var result = await _sender.Send(
            new GetStudentCourseDetailsQuery(userId, courseId),
            cancellationToken);

        return Ok(CommonResponse<StudentCourseDetailsDto>.SuccessResponse(
            result,
            "Course loaded successfully."));
    }

    [HttpPost("{courseId:int}/enroll")]
    public async Task<ActionResult<CommonResponse<StudentCourseDetailsDto>>> Enroll(
        int courseId,
        CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();

        var result = await _sender.Send(
            new EnrollInCourseCommand(userId, courseId),
            cancellationToken);

        return Ok(CommonResponse<StudentCourseDetailsDto>.SuccessResponse(
            result,
            "Course enrollment completed successfully."));
    }

    private int GetCurrentUserId()
    {
        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue("sub");

        if (!int.TryParse(userIdValue, out var userId))
        {
            throw new UnauthorizedAccessException("Invalid user token.");
        }

        return userId;
    }
}