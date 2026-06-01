using IGenServer.Application.Common.Responses;
using IGenServer.Application.Features.AdminSubjects.Commands.CreateSubject;
using IGenServer.Application.Features.AdminSubjects.DTOs;
using IGenServer.Application.Features.AdminSubjects.Queries.GetSubjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IGenServer.API.Controllers;

[ApiController]
[Route("api/admin/subjects")]
[Authorize(Roles = "Admin")]
public sealed class AdminSubjectsController : ControllerBase
{
    private readonly ISender _sender;

    public AdminSubjectsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<CommonResponse<SubjectDto>>> Create(
        CreateSubjectRequestDto request,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new CreateSubjectCommand(
                request.ProgramId,
                request.Name,
                request.Description,
                request.DurationMonths),
            cancellationToken);

        return Ok(CommonResponse<SubjectDto>.SuccessResponse(
            result,
            "Subject created successfully."));
    }

    [HttpGet]
    public async Task<ActionResult<CommonResponse<IReadOnlyList<SubjectDto>>>> GetByProgram(
        [FromQuery] int programId,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetSubjectsQuery(programId), cancellationToken);

        return Ok(CommonResponse<IReadOnlyList<SubjectDto>>.SuccessResponse(
            result,
            "Subjects loaded successfully."));
    }
}