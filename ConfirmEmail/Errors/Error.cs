namespace ConfirmEmail.Errors;

public record Error(EErrorTypes Type, string Code, string Message)
{
    public static readonly Error None = new(EErrorTypes.None, string.Empty, string.Empty);

    public static readonly Error InvalidEmail = new(EErrorTypes.Conflict, "Conflict", "Invalid e-mail");
    
    public static readonly Error WrongEmailOrPassword =
        new(EErrorTypes.Authorization, "Unauthorized", "Wrong e-mail or password");

    public static readonly Error NotVerified = new(EErrorTypes.Validation, "Unauthorized", "Not verified");
}