using ConfirmEmail.DTOs;
using ConfirmEmail.Interfaces;
using ConfirmEmail.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ConfirmEmail.Controllers;

[ApiController]
[Route("api/login")]
public class LoginController(ILoginRepository loginRepository) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await loginRepository.Login(requestDto);

        return result.Match<IActionResult>(
            Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Successfully logged in",
                Result = result.Value,
                StatusCode = StatusCodes.Status200OK
            }),
            Unauthorized(new ApiResponse
            {
                IsSuccess = false,
                Message = result.Error.Message,
                StatusCode = StatusCodes.Status401Unauthorized
            }));
    }

    [HttpGet("verify")]
    public async Task<IActionResult> Verify([FromQuery] string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized("Invalid token");
        }

        var result = await loginRepository.Verify(token);

        return result.Match<IActionResult>(
            Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Successfully verified",
                StatusCode = StatusCodes.Status200OK
            }),
            Unauthorized(new ApiResponse
            {
                IsSuccess = false,
                Message = result.Error.Message,
                StatusCode = StatusCodes.Status401Unauthorized
            }));
    }
}