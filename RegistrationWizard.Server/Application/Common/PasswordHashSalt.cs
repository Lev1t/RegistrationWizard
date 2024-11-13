namespace RegistrationWizard.Server.Application.Common;

public class PasswordHashSalt
{
    public required string PasswordHash { get; init; }
    public required string Salt { get; init; }
}
