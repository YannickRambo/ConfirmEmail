using ConfirmEmail.Models;
using Microsoft.EntityFrameworkCore;

namespace ConfirmEmail.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}