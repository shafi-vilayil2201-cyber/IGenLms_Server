using IGenServer.Application.Common.Responses;
using IGenServer.Application.Features.AdminSubjectCurriculum.Commands.CreateSubjectDayTopic;
using IGenServer.Application.Features.AdminSubjectCurriculum.Commands.CreateSubjectMonth;
using IGenServer.Application.Features.AdminSubjectCurriculum.Commands.CreateSubjectWeek;
using IGenServer.Application.Features.AdminSubjectCurriculum.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IGenServer.API.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
public sealed class AdminSubjectCurriculumController : ControllerBase
{
    private readonly ISender _sender;

    public AdminSubjectCurriculumController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("api/admin/subjects/{subjectId:int}/months")]
    public async Task<ActionResult<CommonResponse<SubjectMonthDto>>> CreateMonth(
        int subjectId,
        CreateSubjectMonthRequestDto request,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new CreateSubjectMonthCommand(subjectId, request.MonthNumber, request.Title),
            cancellationToken);

        return Ok(CommonResponse<SubjectMonthDto>.SuccessResponse(
            result,
            "Subject month created successfully."));
    }

    [HttpPost("api/admin/subject-months/{monthId:int}/weeks")]
    public async Task<ActionResult<CommonResponse<SubjectWeekDto>>> CreateWeek(
        int monthId,
        CreateSubjectWeekRequestDto request,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new CreateSubjectWeekCommand(monthId, request.WeekNumber, request.Title),
            cancellationToken);

        return Ok(CommonResponse<SubjectWeekDto>.SuccessResponse(
            result,
            "Subject week created successfully."));
    }

    [HttpPost("api/admin/subject-weeks/{weekId:int}/day-topics")]
    public async Task<ActionResult<CommonResponse<SubjectDayTopicDto>>> CreateDayTopic(
        int weekId,
        CreateSubjectDayTopicRequestDto request,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new CreateSubjectDayTopicCommand(
                weekId,
                request.DayNumber,
                request.Title,
                request.Description,
                request.EstimatedMinutes),
            cancellationToken);

        return Ok(CommonResponse<SubjectDayTopicDto>.SuccessResponse(
            result,
            "Subject day topic created successfully."));
    }
}