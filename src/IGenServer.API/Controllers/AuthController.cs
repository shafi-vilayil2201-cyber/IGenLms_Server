
using IGenServer.Application.Common.Responses;
using IGenServer.Application.Features.Auth.Commands.RegisterUser;
using IGenServer.Application.Features.Auth.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace IGenServer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<CommonResponse<AuthResponseDto>>> Register(
        [FromBody] RegisterUserRequestDto request,
        [FromServices] RegisterUserCommandHandler handler,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new RegisterUserCommand(
                request.FullName,
                request.Email,
                request.Password,
                request.Role,
                request.TargetYear,
                request.Expertise
            );
            var response = await handler.Handle(command, cancellationToken);

            return Ok(CommonResponse<AuthResponseDto>.SuccessResponse(
                response,"User registered successfully."
            ));

        }
        catch(InvalidOperationException ex)
        {
            return BadRequest(CommonResponse<AuthResponseDto>.FailureResponse(
                "Registration failed.",ex.Message
            ));
        }
    }
}