using IGenServer.Application.Common.Responses;
using IGenServer.Application.Features.AdminPrograms.Commands.CreateProgram;
using IGenServer.Application.Features.AdminPrograms.DTOs;
using IGenServer.Application.Features.AdminPrograms.Queries.GetPrograms;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IGenServer.API.Controllers;

[ApiController]
[Route("api/admin/programs")]
[Authorize(Roles = "Admin")]
public sealed class AdminProgramsController : ControllerBase
{
    private readonly ISender _sender;

    public AdminProgramsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<CommonResponse<ProgramDto>>> Create(
        CreateProgramRequestDto request,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new CreateProgramCommand(request.Name, request.Code),
            cancellationToken);

        return Ok(CommonResponse<ProgramDto>.SuccessResponse(
            result,
            "Program created successfully."));
    }
    [HttpGet]
    public async Task<ActionResult<CommonResponse<IReadOnlyList<ProgramDto>>>> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetProgramsQuery(), cancellationToken);

        return Ok(CommonResponse<IReadOnlyList<ProgramDto>>.SuccessResponse(
            result,
            "Programs loaded successfully."));
    }
}