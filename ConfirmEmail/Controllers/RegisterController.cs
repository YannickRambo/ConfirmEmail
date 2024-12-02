using ConfirmEmail.DTOs;
using ConfirmEmail.Interfaces;
using ConfirmEmail.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ConfirmEmail.Controllers;

[ApiController]
[Route("api/register")]
public class RegisterController(IRegisterRepository registerRepository) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await registerRepository.Register(requestDto);

        return result.Match<IActionResult>(
            Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Successfully registered",
                Result = result.Value,
                StatusCode = StatusCodes.Status200OK
            }),
            BadRequest(new ApiResponse
            {
                IsSuccess = false,
                Message = result.Error.Message,
                StatusCode = StatusCodes.Status400BadRequest
            }));
    }
}