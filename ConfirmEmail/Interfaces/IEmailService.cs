namespace ConfirmEmail.Interfaces;

public interface IEmailService
{
    void Send(string email, string token);
}