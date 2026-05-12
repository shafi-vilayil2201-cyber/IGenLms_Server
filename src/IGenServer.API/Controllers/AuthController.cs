using FluentValidation;
using IGenServer.Application.Common.Responses;
using IGenServer.Application.Features.Auth.Commands.LoginUser;
using IGenServer.Application.Features.Auth.Commands.RegisterUser;
using IGenServer.Application.Features.Auth.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IGenServer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register")]
    public async Task<ActionResult<CommonResponse<AuthResponseDto>>> Register(
        [FromBody] RegisterUserRequestDto request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _sender.Send(
                new RegisterUserCommand(
                    request.FullName,
                    request.Email,
                    request.Password,
                    request.Role,
                    request.TargetYear,
                    request.Expertise),
                cancellationToken);

            return Ok(CommonResponse<AuthResponseDto>.SuccessResponse(
                response,
                "User registered successfully."));
        }
        catch (ValidationException ex)
        {
            return BadRequest(CommonResponse<AuthResponseDto>.FailureResponse(
                "Validation failed.",
                ex.Errors.Select(x => x.ErrorMessage).ToArray()));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(CommonResponse<AuthResponseDto>.FailureResponse(
                "Registration failed.",
                ex.Message));
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<CommonResponse<AuthResponseDto>>> Login(
        [FromBody] LoginUserRequestDto request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _sender.Send(
                new LoginUserCommand(
                    request.Email,
                    request.Password),
                cancellationToken);

            return Ok(CommonResponse<AuthResponseDto>.SuccessResponse(
                response,
                "Login successful."));
        }
        catch (ValidationException ex)
        {
            return BadRequest(CommonResponse<AuthResponseDto>.FailureResponse(
                "Validation failed.",
                ex.Errors.Select(x => x.ErrorMessage).ToArray()));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(CommonResponse<AuthResponseDto>.FailureResponse(
                "Login failed.",
                ex.Message));
        }
    }
}
