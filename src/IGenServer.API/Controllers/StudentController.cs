using System.Security.Claims;
using IGenServer.Application.Common.Responses;
using IGenServer.Application.Features.Students.DTOs;
using IGenServer.Application.Features.Students.Queries.GetStudentDashboard;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IGenServer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Student")]
public sealed class StudentController : ControllerBase
{
    private readonly ISender _sender;

    public StudentController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("dashboard")]
    public async Task<ActionResult<CommonResponse<StudentDashboardResponseDto>>> GetDashboard(
        CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue("sub");

        if (!int.TryParse(userIdClaim, out var userId))
        {
            throw new UnauthorizedAccessException("Invalid user token.");
        }

        var response = await _sender.Send(
            new GetStudentDashboardQuery(userId),
            cancellationToken);

        return Ok(CommonResponse<StudentDashboardResponseDto>.SuccessResponse(
            response,
            "Student dashboard loaded successfully."));
    }
}