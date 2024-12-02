using System.Security.Cryptography;
using ConfirmEmail.Interfaces;

namespace ConfirmEmail.Services;

public class PasswordService : IPasswordService
{
    private const int SaltSize = 16;

    private const int KeySize = 32;

    private const int Iterations = 10000;

    private static readonly HashAlgorithmName HashAlgorithmName = HashAlgorithmName.SHA256;

    private const char Delimeter = ';';

    public string Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName, KeySize);

        return string.Join(Delimeter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
    }

    public bool Verify(string hashedPassword, string inputPassword)
    {
        var elements = hashedPassword.Split(Delimeter);

        var salt = Convert.FromBase64String(elements[0]);
        var hash = Convert.FromBase64String(elements[1]);

        var hashedInput = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, Iterations, HashAlgorithmName, KeySize);

        return CryptographicOperations.FixedTimeEquals(hash, hashedInput);
    }
}