using ConfirmEmail.DTOs;
using ConfirmEmail.Results;

namespace ConfirmEmail.Interfaces;

public interface IRegisterRepository
{
    Task<Result<RegisterRequestDto>> Register(RegisterRequestDto requestDto);
}