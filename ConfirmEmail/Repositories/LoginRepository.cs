using ConfirmEmail.Database;
using ConfirmEmail.DTOs;
using ConfirmEmail.Errors;
using ConfirmEmail.Interfaces;
using ConfirmEmail.Results;
using Microsoft.EntityFrameworkCore;

namespace ConfirmEmail.Repositories;

public class LoginRepository(ApplicationDbContext context, IPasswordService passwordService) : ILoginRepository
{
    public async Task<Result<LoginRequestDto>> Login(LoginRequestDto requestDto)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == requestDto.Email);

        if (user is null)
        {
            return Error.WrongEmailOrPassword;
        }

        bool verify = passwordService.Verify(user.HashedPassword, requestDto.Password);

        if (!verify)
        {
            return Error.WrongEmailOrPassword;
        }

        if (user.VerifiedAt is null)
        {
            return Error.NotVerified;
        }

        return requestDto;
    }

    public async Task<Result> Verify(string token)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);

        if (user is null)
        {
            return Result.Failure(Error.NotVerified);
        }

        user.VerifiedAt = DateTime.Now;
        await context.SaveChangesAsync();

        return Result.Success();
    }
}