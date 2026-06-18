using IGenServer.Application.Common.Responses;
using IGenServer.Application.Features.AdminCourses.Commands.AttachCourseSubjects;
using IGenServer.Application.Features.AdminCourses.Commands.CreateCourse;
using IGenServer.Application.Features.AdminCourses.DTOs;
using IGenServer.Application.Features.AdminCourses.Queries.GetCourseDetails;
using IGenServer.Application.Features.AdminCourses.Queries.GetCourses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IGenServer.API.Controllers;

[ApiController]
[Route("api/admin/courses")]
[Authorize(Roles = "Admin")]
public sealed class AdminCoursesController : ControllerBase
{
    private readonly ISender _sender;

    public AdminCoursesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<CommonResponse<CourseDto>>> Create(
        CreateCourseRequestDto request,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new CreateCourseCommand(
                request.ProgramId,
                request.Title,
                request.Description,
                request.DurationMonths,
                request.Price),
            cancellationToken);

        return Ok(CommonResponse<CourseDto>.SuccessResponse(
            result,
            "Course created successfully."));
    }

    [HttpGet]
    public async Task<ActionResult<CommonResponse<IReadOnlyList<CourseDto>>>> GetByProgram(
        [FromQuery] int programId,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetCoursesQuery(programId), cancellationToken);

        return Ok(CommonResponse<IReadOnlyList<CourseDto>>.SuccessResponse(
            result,
            "Courses loaded successfully."));
    }

    [HttpGet("{courseId:int}")]
    public async Task<ActionResult<CommonResponse<CourseDetailsDto>>> GetDetails(
        int courseId,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetCourseDetailsQuery(courseId), cancellationToken);

        return Ok(CommonResponse<CourseDetailsDto>.SuccessResponse(
            result,
            "Course loaded successfully."));
    }

    [HttpPost("{courseId:int}/subjects")]
    public async Task<ActionResult<CommonResponse<CourseDetailsDto>>> AttachSubjects(
        int courseId,
        AttachCourseSubjectsRequestDto request,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new AttachCourseSubjectsCommand(courseId, request.Subjects),
            cancellationToken);

        return Ok(CommonResponse<CourseDetailsDto>.SuccessResponse(
            result,
            "Course subjects attached successfully."));
    }
}