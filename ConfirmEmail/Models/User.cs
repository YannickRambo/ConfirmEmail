namespace ConfirmEmail.Models;

public class User
{
    public int Id { get; set; }
    
    public string Email { get; set; } = string.Empty;
    
    public string HashedPassword { get; set; } = string.Empty;
   
    public string? VerificationToken { get; set; }
    
    public DateTime? VerifiedAt { get; set; }
}