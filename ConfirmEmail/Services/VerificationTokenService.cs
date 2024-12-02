using System.Security.Cryptography;
using ConfirmEmail.Interfaces;

namespace ConfirmEmail.Services;

public class VerificationTokenService : IVerificationTokenService
{
    public string Generate()
    {
        string randomToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16)); 
        
        return randomToken;
    }
}