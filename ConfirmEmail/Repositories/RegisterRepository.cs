using ConfirmEmail.Database;
using ConfirmEmail.DTOs;
using ConfirmEmail.Errors;
using ConfirmEmail.Interfaces;
using ConfirmEmail.Mappers;
using ConfirmEmail.Results;
using Microsoft.EntityFrameworkCore;

namespace ConfirmEmail.Repositories;

public class RegisterRepository(
    ApplicationDbContext context,
    IPasswordService passwordService,
    IVerificationTokenService tokenService,
    IEmailService emailService) : IRegisterRepository
{
    public async Task<Result<RegisterRequestDto>> Register(RegisterRequestDto requestDto)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == requestDto.Email);

        if (user is not null)
        {
            return Error.InvalidEmail;
        }

        string hashedPassword = passwordService.Hash(requestDto.Password);

        requestDto.Password = hashedPassword;

        string verificationToken = tokenService.Generate();

        requestDto.VerificationToken = verificationToken;

        emailService.Send(requestDto.Email, verificationToken);

        await context.Users.AddAsync(requestDto.MapRegisterRequestDtoToUser());
        await context.SaveChangesAsync();

        return requestDto;
    }
}