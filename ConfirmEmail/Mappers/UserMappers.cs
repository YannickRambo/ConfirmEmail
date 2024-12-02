using ConfirmEmail.DTOs;
using ConfirmEmail.Models;

namespace ConfirmEmail.Mappers;

public static class UserMappers
{
    public static User MapRegisterRequestDtoToUser(this RegisterRequestDto requestDto)
    {
        return new User
        {
            Email = requestDto.Email,
            HashedPassword = requestDto.Password,
            VerificationToken = requestDto.VerificationToken,
        };
    }
}