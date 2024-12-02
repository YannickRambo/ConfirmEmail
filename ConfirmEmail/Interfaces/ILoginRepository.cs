using ConfirmEmail.DTOs;
using ConfirmEmail.Results;

namespace ConfirmEmail.Interfaces;

public interface ILoginRepository
{
     Task<Result<LoginRequestDto>> Login(LoginRequestDto requestDto);
    
     Task<Result> Verify(string token);
}