namespace RegistrationWizard.Server.Domain.Models;

public class UserLogin
{
    public int Id { get; set; }
    public string Login { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string PasswordSalt { get; set; } = null!;


    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
