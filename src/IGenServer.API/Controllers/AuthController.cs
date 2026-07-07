using IGenServer.Application.Common.Responses;
using IGenServer.Application.Features.Auth.Commands.LoginUser;
using IGenServer.Application.Features.Auth.Commands.Logout;
using IGenServer.Application.Features.Auth.Commands.RefreshToken;
using IGenServer.Application.Features.Auth.Commands.RegisterUser;
using IGenServer.Application.Features.Auth.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
            var result = await _sender.Send(
                new RegisterUserCommand(
                    request.FullName,
                    request.Email,
                    request.Password,
                    request.Role,
                    request.TargetYear,
                    request.Expertise),
                cancellationToken);
            SetRefreshTokenCookie(result.RefreshToken);

            return Ok(CommonResponse<AuthResponseDto>.SuccessResponse(
                result.Response,
                "User registered successfully."));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(CommonResponse<AuthResponseDto>.FailureResponse(ex.Message, ex.Message));
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<CommonResponse<AuthResponseDto>>> Login(
        [FromBody] LoginUserRequestDto request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _sender.Send(
                new LoginUserCommand(
                    request.Email,
                    request.Password),
                cancellationToken);

            SetRefreshTokenCookie(result.RefreshToken);

            return Ok(CommonResponse<AuthResponseDto>.SuccessResponse(
                result.Response,
                "Login successful."));
        }
        catch (InvalidOperationException ex)
        {
            return Unauthorized(CommonResponse<AuthResponseDto>.FailureResponse(ex.Message, ex.Message));
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(CommonResponse<AuthResponseDto>.FailureResponse(ex.Message, ex.Message));
        }
    }
    [HttpPost("refresh")]
    public async Task<ActionResult<CommonResponse<AuthResponseDto>>> Refresh(
     CancellationToken cancellationToken)
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            return Unauthorized(CommonResponse<AuthResponseDto>.FailureResponse(
                "Refresh token is missing.",
                "Refresh token is missing."));
        }

        try
        {
            var result = await _sender.Send(
                new RefreshTokenCommand(refreshToken),
                cancellationToken);

            SetRefreshTokenCookie(result.RefreshToken);

            return Ok(CommonResponse<AuthResponseDto>.SuccessResponse(
                result.Response,
                "Token refreshed successfully."));
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(CommonResponse<AuthResponseDto>.FailureResponse(ex.Message, ex.Message));
        }
        catch (InvalidOperationException ex)
        {
            return Unauthorized(CommonResponse<AuthResponseDto>.FailureResponse(ex.Message, ex.Message));
        }
    }
    [HttpPost("logout")]
    public async Task<ActionResult<CommonResponse<string>>> Logout(
        CancellationToken cancellationToken
    )
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if(!string.IsNullOrWhiteSpace(refreshToken))
        {
            await _sender.Send(
                new LogoutCommand(refreshToken),
                cancellationToken
            );
        }
        ClearRefreshTokenCookie();

        return Ok(CommonResponse<string>.SuccessResponse(
            "Logged out successfully.",
            "Logout successful."
        ));
    }

    [Authorize]
    [HttpGet("me")]
    public ActionResult<string> Me()
    {
        return Ok("Authorized");
    }

    private void SetRefreshTokenCookie(string refreshToken)
    {
        Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(7)
        });
    }

    private void ClearRefreshTokenCookie()
    {
        Response.Cookies.Delete("refreshToken");
    }

}
