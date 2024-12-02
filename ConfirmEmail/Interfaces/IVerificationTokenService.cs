namespace ConfirmEmail.Interfaces;

public interface IVerificationTokenService
{
    string Generate();
}