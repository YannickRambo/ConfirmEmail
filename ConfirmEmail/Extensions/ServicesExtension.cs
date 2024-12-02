using ConfirmEmail.Database;
using ConfirmEmail.Interfaces;
using ConfirmEmail.Repositories;
using ConfirmEmail.Services;
using Microsoft.EntityFrameworkCore;

namespace ConfirmEmail.Extensions;

public static class ServicesExtension
{
    public static void AddDatabase(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(8, 0, 25)));
        });
    }
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRegisterRepository, RegisterRepository>();
        services.AddScoped<ILoginRepository, LoginRepository>();
    }
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IVerificationTokenService, VerificationTokenService>();
        services.AddScoped<IEmailService, EmailService>();
    }
}