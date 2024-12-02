using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConfirmEmail.DTOs;

public class RegisterRequestDto
{
    [Required, EmailAddress] public string Email { get; set; } = string.Empty;

    [Required, MinLength(6)] public string Password { get; set; } = string.Empty;
    
    [NotMapped]
    internal string? VerificationToken { get; set; }
}