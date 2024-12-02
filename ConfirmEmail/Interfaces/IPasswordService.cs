namespace ConfirmEmail.Interfaces;

public interface IPasswordService
{
    string Hash(string password);

    bool Verify(string hashedPassword, string inputPassword);
}