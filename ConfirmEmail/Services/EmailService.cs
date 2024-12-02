using System.Net;
using System.Net.Mail;
using ConfirmEmail.Interfaces;

namespace ConfirmEmail.Services;

public class EmailService(IConfiguration configuration) : IEmailService
{
    private const string SmtpServer = "smtp.gmail.com";

    private const int SmtpPort = 587;

    private readonly string? _fromEmail = configuration.GetValue<string>("FromEmail");

    private readonly string? _fromPassword = configuration.GetValue<string>("FromPassword");

    public void Send(string email, string token)
    {
        var mail = new MailMessage();
        mail.From = new MailAddress(_fromEmail);
        mail.To.Add(email);

        mail.Subject = "Confirm e-mail";

        var uri = new Uri($"http://localhost:5162/api/login/verify?token={token}");
        
        mail.Body = $"<a href='{uri}'>Click here to confirm your e-mail</a>";
        mail.IsBodyHtml = true;

        var smtpClient = new SmtpClient(SmtpServer, SmtpPort);
        smtpClient.Credentials = new NetworkCredential(_fromEmail, _fromPassword);
        smtpClient.EnableSsl = true;

        smtpClient.Send(mail);
    }
}